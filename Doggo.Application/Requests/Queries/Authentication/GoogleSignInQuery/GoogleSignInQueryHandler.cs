namespace Doggo.Application.Requests.Queries.Authentication.GoogleSignInQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Responses.Authentication;

public class GoogleSignInQueryHandler : IRequestHandler<GoogleSignInQuery, CommonResult<SignInResponse>>
{
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;


    public GoogleSignInQueryHandler(
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        IGoogleAuthService googleAuthService,
        IUserRepository userRepository,
        UserManager<User> userManager,
        IRefreshTokenRepository refreshTokenRepository
    )
    {
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _googleAuthService = googleAuthService;
        _userRepository = userRepository;
        _userManager = userManager;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<CommonResult<SignInResponse>> Handle(GoogleSignInQuery request, CancellationToken cancellationToken)
    {
        var payload = await _googleAuthService.AuthenticateTokenAsync(request.Credential);

        if (payload is null)
            return Failure<SignInResponse>(UserErrors.UserGoogleAuthorizationFailed);

        var user = await _userRepository.GetUserWithRoles(payload.Email, cancellationToken);

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
            Email = payload.Email,
            UserName = payload.Email,
            GoogleAuth = true,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(userToAdd);

        if (!result.Succeeded)
            return Failure<SignInResponse>(UserErrors.UserCreateFailed);

        var addToRoleResult = await _userManager.AddToRoleAsync(userToAdd, RoleConstants.User);

        if (!addToRoleResult.Succeeded)
        {
            return Failure<SignInResponse>(UserErrors.AddToRoleFailed);
        }

        return Success(new SignInResponse(_jwtTokenGeneratorService.GenerateToken(userToAdd), refreshToken.Token));
    }
}