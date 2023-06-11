namespace Doggo.Application.Requests.Commands.User;

using ResultFactory;
using Doggo.Domain.Entities.User;
using Domain.Results.Abstract;
using Domain.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record SignUpCommand
(
    string Email,
    string Password
) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<SignUpCommand, ICommonResult>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ICommonResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                return ResultFactory.Failure(UserErrors.UserAlreadyExist);
            }

            var userToAdd = new User()
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = "",
                LastName = ""
            };

            var result = await _userManager.CreateAsync(userToAdd, request.Password);

            if (!result.Succeeded)
                return ResultFactory.Failure(UserErrors.UserCreateFailed);

            return ResultFactory.Success();
        }
    }
}