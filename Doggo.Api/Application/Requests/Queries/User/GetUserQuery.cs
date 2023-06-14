namespace Doggo.Application.Requests.Queries.User;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.Entities.User;
using Domain.Results;
using Mappers;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record GetUserQuery(int UserId) : IRequest<CommonResult<GetUserDto>>
{
    public class Handler : IRequestHandler<GetUserQuery, CommonResult<GetUserDto>>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CommonResult<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
                return Failure<GetUserDto>(UserErrors.UserDoesNotExist);

            var getUserDto = user.MapUserToGetUserDto();

            return Success(getUserDto);
        }
    }
};