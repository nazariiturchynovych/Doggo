namespace Doggo.Application.Requests.Queries.Authentication.FacebookSignInQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Responses.Authentication;

public class FacebookSignInQueryHandler : IRequestHandler<FacebookSignInQuery, CommonResult<SignInResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IFacebookAuthService _facebookAuthService;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public FacebookSignInQueryHandler(
        UserManager<User> userManager,
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        IFacebookAuthService facebookAuthService,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository
    )
    {
        _userManager = userManager;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _facebookAuthService = facebookAuthService;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<CommonResult<SignInResponse>> Handle(FacebookSignInQuery request, CancellationToken cancellationToken)
    {
        var result = await _facebookAuthService.AuthenticateTokenAsync(request.AccessToken);

        if (result is null)
        {
            return Failure<SignInResponse>(UserErrors.UserFacebookAuthorizationFailed);
        }

        if (!result.Data.IsValid)
        {
            return Failure<SignInResponse>(UserErrors.UserFacebookAuthorizationFailed);
        }

        var userInfoResult = await _facebookAuthService.GetUserInfoAsync(request.AccessToken);

        var user = await _userRepository.GetUserWithRoles(userInfoResult.Email, cancellationToken);

        var refreshTokenString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var refreshToken = new RefreshToken()
        {
            Token = Guid.NewGuid(),
            Expired = DateTime.Now.AddDays(10)
        };

        await _refreshTokenRepository.AddAsync(refreshToken);
        if (user is not null)
        {
            return Success(new SignInResponse(_jwtTokenGeneratorService.GenerateToken(user), refreshToken.Token));
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
            return Failure<SignInResponse>(UserErrors.UserCreateFailed);

        var addToRoleResult = await _userManager.AddToRoleAsync(userToAdd, RoleConstants.User);

        if (!addToRoleResult.Succeeded)
            return Failure<SignInResponse>(UserErrors.AddToRoleFailed);

        return Success(new SignInResponse(_jwtTokenGeneratorService.GenerateToken(userToAdd), refreshToken.Token));
    }
}