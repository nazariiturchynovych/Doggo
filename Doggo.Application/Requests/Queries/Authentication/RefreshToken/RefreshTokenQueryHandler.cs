namespace Doggo.Application.Requests.Queries.Authentication.RefreshToken;

using System.Security.Claims;
using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Responses.Authentication;

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, CommonResult<SignInResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IFacebookAuthService _facebookAuthService;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenQueryHandler(
        UserManager<User> userManager,
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        IFacebookAuthService facebookAuthService,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _facebookAuthService = facebookAuthService;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }


    public async Task<CommonResult<SignInResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var validatedToken = _jwtTokenGeneratorService.GetClaimsPrincipalFromToken(request.Token);

        if (validatedToken is null)
        {
            return Failure<SignInResponse>("Token is unvalid");
        }

        var user = await _userRepository.GetUserWithRoles(
            Guid.Parse(validatedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value),
            cancellationToken);

        if (user is null)
        {
            return Failure<SignInResponse>("user does not exist");
        }

        var refreshToken = await _refreshTokenRepository.GetAsync(Guid.Parse(request.RefreshToken), cancellationToken);

        if (refreshToken is null)
        {
            return Failure<SignInResponse>("refresh Token is unvalid");
        }

        if (refreshToken.UserId != user.Id)
        {
            return Failure<SignInResponse>("it's not current user token");
        }

        _refreshTokenRepository.Remove(refreshToken);

        var newToken = _jwtTokenGeneratorService.GenerateToken(user);

        var newRefreshToken = new RefreshToken
        {
            Token = Guid.NewGuid(),
            Expired = DateTime.Now.AddDays(10),
            UserId = user.Id
        };

        await _refreshTokenRepository.AddAsync(newRefreshToken);

        await _refreshTokenRepository.SaveChangesAsync();

        return Success(new SignInResponse(newToken, newRefreshToken.Token));

    }
}