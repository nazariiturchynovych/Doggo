namespace Doggo.Application.Requests.Queries.User;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.User;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetUserQuery(Guid UserId) : IRequest<CommonResult<GetUserDto>>
{
    public class Handler : IRequestHandler<GetUserQuery, CommonResult<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;

        public Handler(IUserRepository userRepository, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var cachedUser = await _cacheService.GetData<User>(CacheKeys.User + request.UserId);

            if (cachedUser is null)
            {
                var user = await _userRepository.GetAsync(request.UserId, cancellationToken);

                if (user is null)
                    return Failure<GetUserDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(CacheKeys.User + user.Id, user);

                cachedUser = user;
            }

            var getUserDto = cachedUser.MapUserToGetUserDto();

            return Success(getUserDto);
        }
    }
};