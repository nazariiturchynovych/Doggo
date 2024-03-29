namespace Doggo.Application.Requests.Commands.Authentication.ConfirmResetPasswordCommand;

using System.Net;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class ConfirmResetPasswordCommandHandler : IRequestHandler<ConfirmResetPasswordCommand, CommonResult>
{
    private readonly UserManager<User> _userManager;

    public ConfirmResetPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CommonResult> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded)
            return Failure(UserErrors.ResetPasswordFailed);

        return Success();
    }
}