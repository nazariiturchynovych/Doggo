namespace Doggo.Application.Requests.Commands.User;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record DeleteUserCommand(int UserId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteUserCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CommonResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
                return Failure(UserErrors.UserDoesNotExist);

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return Failure(UserErrors.UserDeleteFailed);

            return Success();
        }
    };
};