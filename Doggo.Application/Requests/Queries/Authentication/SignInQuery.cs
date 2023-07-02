namespace Doggo.Application.Requests.Queries.Authentication;

using Domain.Constants.ErrorConstants;
using Domain.DTO.Authentication;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record SignInQuery
(
    string Email,
    string Password
) : IRequest<CommonResult<SignInDto>>
{
    public class Handler : IRequestHandler<SignInQuery, CommonResult<SignInDto>>
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

        public async Task<CommonResult<SignInDto>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetUserRepository();

            var user = await userRepository.GetUserWithRoles(request.Email, cancellationToken);

            if (user is null)
                return Failure<SignInDto>(CommonErrors.EntityDoesNotExist);

            var logInResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!logInResult)
                return Failure<SignInDto>(UserErrors.PasswordDoesNotMatch);

            return Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }
}