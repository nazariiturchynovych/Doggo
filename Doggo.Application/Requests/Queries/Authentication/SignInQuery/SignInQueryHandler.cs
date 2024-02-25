namespace Doggo.Application.Requests.Queries.Authentication.SignInQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Responses.Authentication;

public class SignInQueryHandler : IRequestHandler<SignInQuery, CommonResult<SignInResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;


    public SignInQueryHandler(
        UserManager<User> userManager,
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository
    )
    {
        _userManager = userManager;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<CommonResult<SignInResponse>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithRoles(request.Email, cancellationToken);

        if (user is null)
            return Failure<SignInResponse>(CommonErrors.EntityDoesNotExist);

        var logInResult = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!logInResult)
            return Failure<SignInResponse>(UserErrors.PasswordDoesNotMatch);

        var refreshToken = new RefreshToken()
        {
            Token = Guid.NewGuid(),
            Expired = DateTime.Now.AddDays(10).ToUniversalTime(),
            UserId = user.Id
        };

        var existedToken = await _refreshTokenRepository.GetByUserIdAsync(user.Id, cancellationToken);

        if (existedToken is not null)
        {
            _refreshTokenRepository.Remove(existedToken);
        }

        await _refreshTokenRepository.AddAsync(refreshToken);

        await _refreshTokenRepository.SaveChangesAsync();

        return Success(new SignInResponse(_jwtTokenGeneratorService.GenerateToken(user), refreshToken.Token));
    }
}