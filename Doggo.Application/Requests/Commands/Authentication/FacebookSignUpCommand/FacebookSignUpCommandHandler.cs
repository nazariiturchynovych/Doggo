namespace Doggo.Application.Requests.Commands.Authentication.FacebookSignUpCommand;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.FacebookAuthService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class FacebookSignUpCommandHandler : IRequestHandler<FacebookSignUpCommand, CommonResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IFacebookAuthService _facebookAuthService;

    public FacebookSignUpCommandHandler(UserManager<User> userManager, IFacebookAuthService facebookAuthService)
    {
        _userManager = userManager;
        _facebookAuthService = facebookAuthService;
    }

    public async Task<CommonResult> Handle(FacebookSignUpCommand request, CancellationToken cancellationToken)
    {
        var result = await _facebookAuthService.AuthenticateTokenAsync(request.AccessToken);

        if (result is null)
        {
            return Failure(UserErrors.UserGoogleAuthorizationFailed);
        }

        if (!result.Data.IsValid)
        {
            return Failure(UserErrors.UserGoogleAuthorizationFailed);
        }

        var userInfoResult = await _facebookAuthService.GetUserInfoAsync(request.AccessToken);


        var user = await _userManager.FindByEmailAsync(userInfoResult.Email);

        if (user is not null)
        {
            return Failure(CommonErrors.EntityAlreadyExist);
        }

        var userToAdd = new User
        {
            FirstName = "",
            LastName = "",
            Email = userInfoResult.Email,
            UserName = userInfoResult.Email,
            FacebookAuth = true,
            EmailConfirmed = true
        };

        var creatingResult = await _userManager.CreateAsync(userToAdd);

        if (!creatingResult.Succeeded)
            return Failure(UserErrors.UserCreateFailed);

        var addToRoleResult = await _userManager.AddToRoleAsync(userToAdd, RoleConstants.User);

        if (!addToRoleResult.Succeeded)
            return Failure(UserErrors.AddToRoleFailed);


        return Success();
    }
}