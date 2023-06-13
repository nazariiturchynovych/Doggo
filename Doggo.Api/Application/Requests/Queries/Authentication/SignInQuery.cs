namespace Doggo.Application.Requests.Queries.Authentication;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.Entities.User;
using Domain.Results.Abstract;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record SignInQuery
(
    string Email,
    string Password
) : IRequest<ICommonResult<SignInDto>>
{
    public class Handler : IRequestHandler<SignInQuery, ICommonResult<SignInDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(UserManager<User> userManager, IJwtTokenGeneratorService jwtTokenGeneratorService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommonResult<SignInDto>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetUserRepository();

            var user = await userRepository.GetUserWithRoles(request.Email, cancellationToken);

            if (user is null)
                return Failure<SignInDto>(UserErrors.UserDoesNotExist);

            var logInResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!logInResult)
                return Failure<SignInDto>(UserErrors.PasswordDoesNotMatch);

            return Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }
}