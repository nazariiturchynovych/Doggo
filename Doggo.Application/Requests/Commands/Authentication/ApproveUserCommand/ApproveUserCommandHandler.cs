namespace Doggo.Application.Requests.Commands.Authentication.ApproveUserCommand;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, CommonResult>
{
    private readonly UserManager<User> _userManager;

    public ApproveUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CommonResult> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        if (!user.EmailConfirmed)
            return Failure(UserErrors.EmailIsNotConfirmed);

        if (user.IsApproved)
            return Failure(UserErrors.UserIsAlreadyConfirmed);

        user.IsApproved = true;

        await _userManager.UpdateAsync(user);

        return Success();
    }
}