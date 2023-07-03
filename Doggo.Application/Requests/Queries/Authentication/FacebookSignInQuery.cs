namespace Doggo.Application.Requests.Queries.Authentication;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Authentication;
using Infrastructure.Services.FacebookAuthService;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;

public record FacebookSignInQuery(string AccessToken) : IRequest<CommonResult<SignInDto>>
{
    public class Handler : IRequestHandler<FacebookSignInQuery, CommonResult<SignInDto>>
    {
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IUserRepository _userRepository;

        public Handler(
            IJwtTokenGeneratorService jwtTokenGeneratorService,
            IFacebookAuthService facebookAuthService,
            IUserRepository userRepository)
        {
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _facebookAuthService = facebookAuthService;
            _userRepository = userRepository;
        }

        public async Task<CommonResult<SignInDto>> Handle(FacebookSignInQuery request, CancellationToken cancellationToken)
        {
            var result = await _facebookAuthService.AuthenticateTokenAsync(request.AccessToken);

            if (result is null)
            {
                return Failure<SignInDto>(UserErrors.UserFacebookAuthorizationFailed);
            }

            if (!result.Data.IsValid)
            {
                return Failure<SignInDto>(UserErrors.UserFacebookAuthorizationFailed);
            }

            var userInfoResult = await _facebookAuthService.GetUserInfoAsync(request.AccessToken);

            var user = await _userRepository.GetUserWithRoles(userInfoResult.Email, cancellationToken);

            return user is null
                ? Failure<SignInDto>(CommonErrors.EntityDoesNotExist)
                : Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }
}