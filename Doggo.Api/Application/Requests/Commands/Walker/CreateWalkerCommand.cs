namespace Doggo.Application.Requests.Commands.Walker;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Walker;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record CreateWalkerCommand(string Skills, string About) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateWalkerCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;

        public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<CommonResult> Handle(CreateWalkerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetWalkerRepository();

            var walker = await repository.GetAsync(_currentUserService.GetUserId(), cancellationToken);

            if (walker is not null)
                return Failure(CommonErrors.EntityAlreadyExist);

            await repository.AddAsync(
                new Walker()
                {
                    Skills = request.Skills,
                    About = request.About,
                    UserId = _currentUserService.GetUserId()
                });

            await _userManager.AddToRoleAsync(
                (await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString()))!,
                RoleConstants.Walker);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}