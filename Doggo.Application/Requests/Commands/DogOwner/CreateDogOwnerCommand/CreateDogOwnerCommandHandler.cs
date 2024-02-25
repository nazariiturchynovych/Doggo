namespace Doggo.Application.Requests.Commands.DogOwner.CreateDogOwnerCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.DogOwner;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class CreateDogOwnerCommandHandler : IRequestHandler<CreateDogOwnerCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<User> _userManager;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public CreateDogOwnerCommandHandler(
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

         await _dogOwnerRepository.SaveChangesAsync();
        return Success();
    }
}