namespace Doggo.Application.Requests.Commands.Authentication;

using System.Net;
using Domain.Entities.User;
using Domain.Results.Abstract;
using Domain.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultFactory;

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

            var token = WebUtility.UrlDecode(request.Token);

            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!result.Succeeded)
                return ResultFactory.Failure(UserErrors.ResetPasswordFailed);

            return ResultFactory.Success();
        }
    }
}