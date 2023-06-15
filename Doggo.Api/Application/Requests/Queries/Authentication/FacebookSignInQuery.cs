namespace Doggo.Application.Requests.Queries.Authentication;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.Results;
using Helpers;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.FacebookAuthService;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;
using User;

public record FacebookSignInQuery(string AccessToken) : IRequest<CommonResult<GetUserDto>>
{
    public class Handler : IRequestHandler<FacebookSignInQuery, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IFacebookAuthService _facebookAuthService;

        public Handler(IUnitOfWork unitOfWork, IJwtTokenGeneratorService jwtTokenGeneratorService, IFacebookAuthService facebookAuthService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _facebookAuthService = facebookAuthService;
        }

        public async Task<CommonResult> Handle(FacebookSignInQuery request, CancellationToken cancellationToken)
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

            var userRepository = _unitOfWork.GetUserRepository();

            var user = await userRepository.GetUserWithRoles(userInfoResult.Email, cancellationToken);

            if (user is null)
                return Failure<SignInDto>(UserErrors.UserDoesNotExist);


            return Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }
}