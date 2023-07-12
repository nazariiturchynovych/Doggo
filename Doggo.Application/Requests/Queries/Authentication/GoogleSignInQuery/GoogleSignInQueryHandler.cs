namespace Doggo.Application.Requests.Queries.Authentication.GoogleSignInQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;
using Responses.Authentication;

public class GoogleSignInQueryHandler : IRequestHandler<GoogleSignInQuery, CommonResult<SignInResponse>>
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

    public async Task<CommonResult<SignInResponse>> Handle(GoogleSignInQuery request, CancellationToken cancellationToken)
    {
        var payload = await _googleAuthService.AuthenticateTokenAsync(request.Credential);

        if (payload is null)
            return Failure<SignInResponse>(UserErrors.UserGoogleAuthorizationFailed);

        var user = await _userRepository.GetUserWithRoles(payload.Email, cancellationToken);

        if (user is null)
            return Failure<SignInResponse>(UserErrors.UserDoesNotExist);

        if (!user.IsApproved)
            return Failure<SignInResponse>(UserErrors.UserIsNotApproved);

        return Success(new SignInResponse(_jwtTokenGeneratorService.GenerateToken(user)));
    }
}