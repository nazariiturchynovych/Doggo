namespace Doggo.Application.Requests.Commands.Authentication;

using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record GoogleSignUpCommand(string Credential) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<GoogleSignUpCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IGoogleAuthService _googleAuthService;

        public Handler(UserManager<User> userManager, IGoogleAuthService googleAuthService)
        {
            _userManager = userManager;
            _googleAuthService = googleAuthService;
        }

        public async Task<CommonResult> Handle(GoogleSignUpCommand request, CancellationToken cancellationToken)
        {
            var payload = await _googleAuthService.AuthenticateTokenAsync(request.Credential);

            if (payload is null)
                return Failure(UserErrors.UserGoogleAuthorizationFailed);

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user is not null)
                return Failure(CommonErrors.EntityAlreadyExist);

            var userToAdd = new User
            {
                FirstName = "",
                LastName = "",
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