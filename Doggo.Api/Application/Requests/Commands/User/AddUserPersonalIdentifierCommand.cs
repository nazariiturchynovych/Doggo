namespace Doggo.Application.Requests.Commands.User;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Entities.User.Documents;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record AddUserPersonalIdentifierCommand(
    PersonalIdentifierType IdentifierType
) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<AddUserPersonalIdentifierCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(UserManager<User> userManager, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(AddUserPersonalIdentifierCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());

            if (currentUser is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var repository = _unitOfWork.GetPersonalIdentifierRepository();

            await repository.AddAsync(
                new PersonalIdentifier()
                {
                    UserId = _currentUserService.GetUserId(),
                    PersonalIdentifierType = request.IdentifierType
                });

            return Success();
        }
    }
}