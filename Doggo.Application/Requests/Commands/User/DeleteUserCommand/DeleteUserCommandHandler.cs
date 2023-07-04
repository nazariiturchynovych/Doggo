namespace Doggo.Application.Requests.Commands.User.DeleteUserCommand;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommonResult>
{
    private readonly UserManager<User> _userManager;

    public DeleteUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CommonResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
            return Failure(CommonErrors.EntityDeleteFailed);

        return Success();
    }
}