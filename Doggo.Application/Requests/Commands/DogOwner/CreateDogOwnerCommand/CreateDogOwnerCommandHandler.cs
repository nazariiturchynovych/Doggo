namespace Doggo.Application.Requests.Commands.DogOwner.CreateDogOwnerCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.DogOwner;
using Domain.Entities.User;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class CreateDogOwnerCommandHandler : IRequestHandler<CreateDogOwnerCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<User> _userManager;
    private readonly IDogOwnerRepository _dogOwnerRepository;
    private readonly IWalkerRepository _walkerRepository;

    public CreateDogOwnerCommandHandler(
        ICurrentUserService currentUserService,
        UserManager<User> userManager,
        IDogOwnerRepository dogOwnerRepository,
        IWalkerRepository walkerRepository)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _dogOwnerRepository = dogOwnerRepository;
        _walkerRepository = walkerRepository;
    }

    public async Task<CommonResult> Handle(CreateDogOwnerCommand request, CancellationToken cancellationToken)
    {
        var walker = _walkerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);
        if (walker is not null)
            return Failure(DogOwnerErrors.UserIsAlreadyWalker);

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