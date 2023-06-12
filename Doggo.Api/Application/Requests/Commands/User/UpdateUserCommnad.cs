namespace Doggo.Application.Requests.Commands.User;

using Domain.Entities.User;
using Domain.Results.Abstract;
using Domain.Results.Errors;
using Infrastructure.Services.CurrentUserService;
using Mappers;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record UpdateUserCommand
(
    string? FirstName,
    string? LastName,
    int? Age
) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<UpdateUserCommand, ICommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public Handler(UserManager<User> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<ICommonResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByIdAsync(_currentUserService.GetUserId());

            if (currentUser is null)
                return Failure(CommonErrors.InnerError);

            var updatedUser = request.MapUserUpdateCommandToUser(currentUser);

            var result = await _userManager.UpdateAsync(updatedUser);

            if (!result.Succeeded)
                return Failure(UserErrors.UserUpdateFailed);

            return Success();
        }
    }
}