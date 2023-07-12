namespace Doggo.Application.Requests.Queries.Authentication.FacebookSignInQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;
using Responses.Authentication;

public class FacebookSignInQueryHandler : IRequestHandler<FacebookSignInQuery, CommonResult<SignInResponse>>
{
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IFacebookAuthService _facebookAuthService;
    private readonly IUserRepository _userRepository;

    public FacebookSignInQueryHandler(
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        IFacebookAuthService facebookAuthService,
        IUserRepository userRepository)
    {
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _facebookAuthService = facebookAuthService;
        _userRepository = userRepository;
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

        if (user is null)
            return Failure<SignInResponse>(UserErrors.UserDoesNotExist);

        if (!user.IsApproved)
            return Failure<SignInResponse>(UserErrors.UserIsNotApproved);

        return Success(new SignInResponse(_jwtTokenGeneratorService.GenerateToken(user)));
    }
}