namespace Doggo.Application.Requests.Commands.Authentication;

using Domain.Entities.User;
using Domain.Results.Abstract;
using Domain.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultFactory;

public record GoogleSignUpCommand(string Email) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<GoogleSignUpCommand, ICommonResult>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ICommonResult> Handle(GoogleSignUpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                return ResultFactory.Failure(UserErrors.UserAlreadyExist);
            }

            var userToAdd = new User
            {
                Email = request.Email,
                UserName = request.Email,
                GoogleAuth = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(userToAdd);

            if (!result.Succeeded)
                return ResultFactory.Failure(UserErrors.UserCreateFailed);

            return ResultFactory.Success();
        }
    }
}