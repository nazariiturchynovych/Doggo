namespace Doggo.Application.Requests.Commands.DogOwner;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.DogOwner;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record CreateDogOwnerCommand(string Address, string District) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateDogOwnerCommand, CommonResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        private readonly IDogOwnerRepository _dogOwnerRepository;

        public Handler(
            ICurrentUserService currentUserService,
            UserManager<User> userManager,
            IDogOwnerRepository dogOwnerRepository)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _dogOwnerRepository = dogOwnerRepository;
        }

        public async Task<CommonResult> Handle(CreateDogOwnerCommand request, CancellationToken cancellationToken)
        {
            var dogOwner = await _dogOwnerRepository.GetAsync(_currentUserService.GetUserId(), cancellationToken);

            if (dogOwner is not null)
                return Failure(CommonErrors.EntityAlreadyExist);

            await _dogOwnerRepository.AddAsync(
                new DogOwner()
                {
                    Address = request.Address,
                    District = request.District,
                    UserId = _currentUserService.GetUserId()
                });

            await _userManager.AddToRoleAsync(
                (await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString()))!,
                RoleConstants.DogOwner);

            return Success();
        }
    }
}