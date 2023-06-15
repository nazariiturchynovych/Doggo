namespace Doggo.Application.Requests.Commands.Authentication;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record GoogleSignUpCommand(string Token) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<GoogleSignUpCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CommonResult> Handle(GoogleSignUpCommand request, CancellationToken cancellationToken)
        {
            var payload = await GoogleAuthHelper.AuthenticateTokenAsync(request.Token);

            if (payload is null)
            {
                return Failure(UserErrors.UserGoogleAuthorizationFailed);
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user is not null)
            {
                return Failure(UserErrors.UserAlreadyExist);
            }

            var userToAdd = new User
            {
                FirstName = payload.Name,
                LastName = payload.GivenName,
                Email = payload.Email,
                UserName = payload.Email,
                GoogleAuth = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(userToAdd);

            if (!result.Succeeded)
                return Failure(UserErrors.UserCreateFailed);

            var addToRoleResult = await _userManager.AddToRoleAsync(userToAdd, RoleConstants.User);

            if (!addToRoleResult.Succeeded)
            {
                return Failure(UserErrors.AddToRoleFailed);
            }

            return Success();
        }
    }
}