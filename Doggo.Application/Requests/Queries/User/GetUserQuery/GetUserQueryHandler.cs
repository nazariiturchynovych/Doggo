namespace Doggo.Application.Requests.Queries.User.GetUserQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.User;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, CommonResult<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICacheService _cacheService;

    public GetUserQueryHandler(IUserRepository userRepository, ICacheService cacheService)
    {
        _userRepository = userRepository;
        _cacheService = cacheService;
    }

    public async Task<CommonResult<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var cachedUser = await _cacheService.GetData<User>(CacheKeys.User + request.UserId, cancellationToken);

        if (cachedUser is null)
        {
            var user = await _userRepository.GetWithPersonalIdentifierAsync(request.UserId, cancellationToken);

            if (user is null)
                return Failure<UserResponse>(CommonErrors.EntityDoesNotExist);

            await _cacheService.SetData(CacheKeys.User + user.Id, user, cancellationToken);

            cachedUser = user;
        }

        var getUserDto = cachedUser.MapUserToUserResponse();

        return Success(getUserDto);
    }
}