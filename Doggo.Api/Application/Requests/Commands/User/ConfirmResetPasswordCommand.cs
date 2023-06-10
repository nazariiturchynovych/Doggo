namespace Doggo.Application.Requests.Commands.User;

using ResultFactory;
using Doggo.Domain.Entities.User;
using Doggo.Domain.Results.Abstract;
using Doggo.Domain.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record ConfirmResetPasswordCommand(string Token, string UserId, string NewPassword) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<ConfirmResetPasswordCommand, ICommonResult>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ICommonResult> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                return ResultFactory.Failure(UserErrors.UserDoesNotExist);

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (result.Succeeded!)
                return ResultFactory.Failure(UserErrors.ResetPasswordFailed);

            return ResultFactory.Success();
        }
    }
}