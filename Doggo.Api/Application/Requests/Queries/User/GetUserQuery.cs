namespace Doggo.Application.Requests.Queries.User;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record GetUserQuery(int UserId) : IRequest<CommonResult<GetUserDto>>
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
            var cachedUser = await _cacheService.GetData<User>(request.UserId.ToString());

            if (cachedUser is null)
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user is null)
                    return Failure<GetUserDto>(UserErrors.UserDoesNotExist);

                await _cacheService.SetData(user.Id.ToString(), user);

                cachedUser = user;
            }

            var getUserDto = cachedUser.MapUserToGetUserDto();

            return Success(getUserDto);
        }
    }
};