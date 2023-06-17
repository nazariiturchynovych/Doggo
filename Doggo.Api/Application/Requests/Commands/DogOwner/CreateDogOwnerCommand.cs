namespace Doggo.Application.Requests.Commands.DogOwner;

using Domain.Constants;
using Domain.Entities.DogOwner;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record CreateDogOwnerCommand(string Address, string District) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateDogOwnerCommand, CommonResult>
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

        public async Task<CommonResult> Handle(CreateDogOwnerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogOwnerRepository();

            var dogOwner = await repository.GetAsync(_currentUserService.GetUserId(), cancellationToken);

            if (dogOwner is not null)
                return Failure("some text"); //TODO change to some const

            await repository.AddAsync(
                new DogOwner()
                {
                    Address = request.Address,
                    District = request.District,
                    UserId = _currentUserService.GetUserId()
                });

            await _userManager.AddToRoleAsync(
                (await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString()))!,
                RoleConstants.DogOwner);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}