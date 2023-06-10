namespace Doggo.Application.Requests.Commands.User;

using ResultFactory;
using Doggo.Domain.Entities.User;
using Doggo.Domain.Results.Abstract;
using Doggo.Domain.Results.Errors;
using Infrastructure.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record ChangePasswordCommand
(
    string CurrentPassword,
    string NewPassword
) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<ChangePasswordCommand, ICommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public Handler(UserManager<User> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<ICommonResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = _currentUserService.GetUserEmail();

            if (currentUserEmail is null)
                return ResultFactory.Failure(CommonErrors.InnerError);

            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            if (currentUser is null)
                return ResultFactory.Failure(UserErrors.UserDoesNotExist);

            var passwordCheckingResult = await _userManager.CheckPasswordAsync(currentUser, request.CurrentPassword);

            if (passwordCheckingResult!)
                return ResultFactory.Failure(UserErrors.PasswordDoesNotMatch);

            var passwordChangingResult = await _userManager.ChangePasswordAsync(
                currentUser,
                request.CurrentPassword,
                request.NewPassword);

            if (passwordChangingResult.Succeeded!)
                return ResultFactory.Failure(UserErrors.PasswordChangeFailed);

            return ResultFactory.Success();
        }
    }
}