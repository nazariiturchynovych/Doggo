namespace Doggo.Application.Requests.Queries.Authentication;

using Domain.DTO;
using Domain.Entities.User;
using Domain.Results.Abstract;
using Domain.Results.Errors;
using Infrastructure.JWTTokenGeneratorService;
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

        public Handler(UserManager<User> userManager, IJwtTokenGeneratorService jwtTokenGeneratorService)
        {
            _userManager = userManager;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
        }
        public async Task<ICommonResult<SignInDto>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return Failure<SignInDto>(UserErrors.UserDoesNotExist);

            var logInResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!logInResult)
                return Failure<SignInDto>(UserErrors.PasswordDoesNotMatch);

            return Success(
                new SignInDto(_jwtTokenGeneratorService.GenerateToken(user)));
        }
    }

}