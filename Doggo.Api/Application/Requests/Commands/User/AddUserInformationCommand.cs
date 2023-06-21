namespace Doggo.Application.Requests.Commands.User;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using Mappers;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record AddUserInformationCommand
(
    string FirstName,
    string LastName,
    int Age,
    string PhoneNumber
) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<AddUserInformationCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public Handler(UserManager<User> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<CommonResult> Handle(AddUserInformationCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());

            if (currentUser is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedUser = request.MapAddUserInformationCommandToUser(currentUser);

            var result = await _userManager.UpdateAsync(updatedUser);

            if (!result.Succeeded)
                return Failure(CommonErrors.EntityUpdateFailed);

            return Success();
        }
    }
}