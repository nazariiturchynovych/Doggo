namespace Doggo.Application.Requests.Commands.Walker;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Walker;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record CreateWalkerCommand(string Skills, string About) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateWalkerCommand, CommonResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        private readonly IWalkerRepository _walkerRepository;

        public Handler(
            ICurrentUserService currentUserService,
            UserManager<User> userManager,
            IWalkerRepository walkerRepository)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _walkerRepository = walkerRepository;
        }

        public async Task<CommonResult> Handle(CreateWalkerCommand request, CancellationToken cancellationToken)
        {
            var walker = await _walkerRepository.GetAsync(_currentUserService.GetUserId(), cancellationToken);

            if (walker is not null)
                return Failure(CommonErrors.EntityAlreadyExist);

            await _walkerRepository.AddAsync(
                new Walker()
                {
                    Skills = request.Skills,
                    About = request.About,
                    UserId = _currentUserService.GetUserId()
                });

            await _userManager.AddToRoleAsync(
                (await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString()))!,
                RoleConstants.Walker);

            return Success();
        }
    }
}