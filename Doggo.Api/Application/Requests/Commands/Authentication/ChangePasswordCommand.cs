namespace Doggo.Application.Requests.Commands.Authentication;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record ChangePasswordCommand
(
    string CurrentPassword,
    string NewPassword
) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<ChangePasswordCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public Handler(UserManager<User> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<CommonResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = _currentUserService.GetUserEmail();

            if (currentUserEmail is null)
                return Failure(CommonErrors.InnerError);

            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            if (currentUser is null)
                return Failure(UserErrors.UserDoesNotExist);

            var passwordCheckingResult = await _userManager.CheckPasswordAsync(currentUser, request.CurrentPassword);

            if (passwordCheckingResult!)
                return Failure(UserErrors.PasswordDoesNotMatch);

            var passwordChangingResult = await _userManager.ChangePasswordAsync(
                currentUser,
                request.CurrentPassword,
                request.NewPassword);

            if (!passwordChangingResult.Succeeded)
                return Failure(UserErrors.PasswordChangeFailed);

            return Success();
        }
    }
}