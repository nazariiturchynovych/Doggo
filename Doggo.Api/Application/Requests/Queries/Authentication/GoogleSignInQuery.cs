namespace Doggo.Application.Requests.Queries.Authentication;

using Domain.Constants.ErrorConstants;
using Domain.DTO.Authentication;
using Domain.Results;
using Helpers;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;

public record GoogleSignInQuery(string Token) : IRequest<CommonResult<SignInDto>>
{
    public class Handler : IRequestHandler<GoogleSignInQuery, CommonResult<SignInDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;

        public Handler(IUnitOfWork unitOfWork, IJwtTokenGeneratorService jwtTokenGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
        }

        public async Task<CommonResult<SignInDto>> Handle(GoogleSignInQuery request, CancellationToken cancellationToken)
        {
            var payload = await GoogleAuthHelper.AuthenticateTokenAsync(request.Token);

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