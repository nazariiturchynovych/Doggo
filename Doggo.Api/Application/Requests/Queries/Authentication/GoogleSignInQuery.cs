namespace Doggo.Application.Requests.Queries.Authentication;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.Entities.User;
using Domain.Results;
using Helpers;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record GoogleSignInQuery(string token) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<GoogleSignInQuery, CommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly JwtTokenGeneratorService _jwtTokenGeneratorService;

        public Handler(UserManager<User> userManager, UnitOfWork unitOfWork, JwtTokenGeneratorService jwtTokenGeneratorService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
        }

        public async Task<CommonResult> Handle(GoogleSignInQuery request, CancellationToken cancellationToken)
        {
            var payload = await GoogleAuthHelper.AuthenticateTokenAsync(request.token);

            if (payload is null)
            {
                return Failure(UserErrors.UserGoogleAuthorizationFailed);
            }

            var userRepository = _unitOfWork.GetUserRepository();

            var user = await userRepository.GetUserWithRoles(payload.Email, cancellationToken);

            if (user is null)
                return Failure<SignInDto>(UserErrors.UserDoesNotExist);


            return Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }
}