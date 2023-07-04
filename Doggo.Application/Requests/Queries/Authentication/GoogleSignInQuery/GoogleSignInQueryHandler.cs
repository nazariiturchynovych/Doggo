namespace Doggo.Application.Requests.Queries.Authentication.GoogleSignInQuery;

using Abstractions.Persistence.Read;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Authentication;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;

public class GoogleSignInQueryHandler : IRequestHandler<GoogleSignInQuery, CommonResult<SignInDto>>
{
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IUserRepository _userRepository;


    public GoogleSignInQueryHandler(
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        IGoogleAuthService googleAuthService,
        IUserRepository userRepository)
    {
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _googleAuthService = googleAuthService;
        _userRepository = userRepository;
    }

    public async Task<CommonResult<SignInDto>> Handle(GoogleSignInQuery request, CancellationToken cancellationToken)
    {
        var payload = await _googleAuthService.AuthenticateTokenAsync(request.Credential);

        if (payload is null)
            return Failure<SignInDto>(UserErrors.UserGoogleAuthorizationFailed);

        var user = await _userRepository.GetUserWithRoles(payload.Email, cancellationToken);

        return user is null
            ? Failure<SignInDto>(CommonErrors.EntityDoesNotExist)
            : Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
    }
}