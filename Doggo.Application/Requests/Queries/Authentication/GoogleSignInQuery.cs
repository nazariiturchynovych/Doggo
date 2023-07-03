namespace Doggo.Application.Requests.Queries.Authentication;

using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Authentication;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;

public record GoogleSignInQuery(string Credential) : IRequest<CommonResult<SignInDto>>
{
    public class Handler : IRequestHandler<GoogleSignInQuery, CommonResult<SignInDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IGoogleAuthService _googleAuthService;

        public Handler(IUnitOfWork unitOfWork, IJwtTokenGeneratorService jwtTokenGeneratorService, IGoogleAuthService googleAuthService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _googleAuthService = googleAuthService;
        }

        public async Task<CommonResult<SignInDto>> Handle(GoogleSignInQuery request, CancellationToken cancellationToken)
        {
            var payload = await _googleAuthService.AuthenticateTokenAsync(request.Credential);

            if (payload is null)
                return Failure<SignInDto>(UserErrors.UserGoogleAuthorizationFailed);

            var userRepository = _unitOfWork.GetUserRepository();

            var user = await userRepository.GetUserWithRoles(payload.Email, cancellationToken);

            return user is null ?
                Failure<SignInDto>(CommonErrors.EntityDoesNotExist) :
                Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }
}