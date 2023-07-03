namespace Doggo.Application.Requests.Commands.User;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Entities.User.Documents;
using Domain.Enums;
using Domain.Results;
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
        private readonly IPersonalIdentifierRepository _personalIdentifierRepository;

        public Handler(
            UserManager<User> userManager,
            ICurrentUserService currentUserService,
            IPersonalIdentifierRepository personalIdentifierRepository)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _personalIdentifierRepository = personalIdentifierRepository;
        }

        public async Task<CommonResult> Handle(AddUserPersonalIdentifierCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());

            if (currentUser is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            await _personalIdentifierRepository.AddAsync(
                new PersonalIdentifier()
                {
                    UserId = _currentUserService.GetUserId(),
                    PersonalIdentifierType = request.IdentifierType
                });

            return Success();
        }
    }
}