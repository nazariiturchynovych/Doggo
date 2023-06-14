namespace Doggo.Application.Requests.Commands.Authentication;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record SignUpCommand
(
    string Email,
    string Password
) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<SignUpCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CommonResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                return Failure(UserErrors.UserAlreadyExist);
            }

            var userToAdd = new User
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = "",
                LastName = ""
            };

            var result = await _userManager.CreateAsync(userToAdd, request.Password);

            if (!result.Succeeded)
                return Failure(UserErrors.UserCreateFailed);

            return Success();
        }
    }
}