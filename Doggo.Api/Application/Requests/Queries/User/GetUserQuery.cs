namespace Doggo.Application.Requests.Queries.User;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.User;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record GetUserQuery(Guid UserId) : IRequest<CommonResult<GetUserDto>>
{
    public class Handler : IRequestHandler<GetUserQuery, CommonResult<GetUserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICacheService _cacheService;

        public Handler(UserManager<User> userManager, ICacheService cacheService)
        {
            _userManager = userManager;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var cachedUser = await _cacheService.GetData<User>(CacheKeys.User + request.UserId);

            if (cachedUser is null)
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

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