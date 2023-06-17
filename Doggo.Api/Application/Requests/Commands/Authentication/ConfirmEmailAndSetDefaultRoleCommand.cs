namespace Doggo.Application.Requests.Commands.Authentication;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record ConfirmEmailAndSetDefaultRoleCommand
    (int UserId, string Token) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<ConfirmEmailAndSetDefaultRoleCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public Handler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<CommonResult> Handle(ConfirmEmailAndSetDefaultRoleCommand request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetUserRepository();

            var user = await userRepository.GetUserWithRoles(request.UserId, cancellationToken);

            if (user is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            if (user.EmailConfirmed)
                return Failure(UserErrors.EmailAlreadyConfirmed);

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
                return Failure(UserErrors.UserEmailConfirmFailed);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, RoleConstants.User);

            if (!addToRoleResult.Succeeded)
            {
                return Failure(UserErrors.AddToRoleFailed);
            }

            return Success();
        }
    }
}