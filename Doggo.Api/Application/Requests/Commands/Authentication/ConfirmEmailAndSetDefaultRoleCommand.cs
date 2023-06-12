namespace Doggo.Application.Requests.Commands.Authentication;

using Domain.Entities.User;
using Domain.Enums;
using Domain.Results.Abstract;
using Domain.Results.Errors;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultFactory;

public record ConfirmEmailAndSetDefaultRoleCommand
    (int UserId, string Token) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<ConfirmEmailAndSetDefaultRoleCommand, ICommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public Handler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ICommonResult> Handle(ConfirmEmailAndSetDefaultRoleCommand request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetUserRepository();

            var user = await userRepository.GetUserWithRoles(request.UserId, cancellationToken);

            if (user is null)
                return ResultFactory.Failure(UserErrors.UserDoesNotExist);

            if (user.EmailConfirmed)
                return ResultFactory.Failure(UserErrors.EmailAlreadyConfirmed);

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
                return ResultFactory.Failure(UserErrors.UserEmailConfirmFailed);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, RoleType.User.ToString().ToUpperInvariant());

            if (!addToRoleResult.Succeeded)
            {
                return ResultFactory.Failure(UserErrors.UserCreateFailed);
            }

            return ResultFactory.Success();
        }
    }
}