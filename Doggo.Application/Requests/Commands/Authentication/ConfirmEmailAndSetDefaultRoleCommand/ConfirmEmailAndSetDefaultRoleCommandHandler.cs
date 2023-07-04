namespace Doggo.Application.Requests.Commands.Authentication.ConfirmEmailAndSetDefaultRoleCommand;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class ConfirmEmailAndSetDefaultRoleCommandHandler : IRequestHandler<ConfirmEmailAndSetDefaultRoleCommand, CommonResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public ConfirmEmailAndSetDefaultRoleCommandHandler(UserManager<User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<CommonResult> Handle(ConfirmEmailAndSetDefaultRoleCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetUserWithRoles(request.UserId, cancellationToken);

        if (user is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        if (user.EmailConfirmed)
            return Failure(UserErrors.EmailAlreadyConfirmed);

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
            return Failure(UserErrors.UserEmailConfirmFailed);

        var addToRoleResult = await _userManager.AddToRoleAsync(user, RoleConstants.User);

        if (!addToRoleResult.Succeeded)
            return Failure(UserErrors.AddToRoleFailed);

        return Success();
    }
}