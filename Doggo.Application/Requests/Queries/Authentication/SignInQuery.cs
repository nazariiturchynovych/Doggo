namespace Doggo.Application.Requests.Queries.Authentication;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using DTO.Authentication;
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
        private readonly IUserRepository _userRepository;


        public Handler(
            UserManager<User> userManager,
            IJwtTokenGeneratorService jwtTokenGeneratorService,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _userRepository = userRepository;
        }

        public async Task<CommonResult<SignInDto>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithRoles(request.Email, cancellationToken);

            if (user is null)
                return Failure<SignInDto>(CommonErrors.EntityDoesNotExist);

            var logInResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!logInResult)
                return Failure<SignInDto>(UserErrors.PasswordDoesNotMatch);

            return Success(new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }
}