namespace Doggo.Application.Requests.Commands.Walker.CreateWalkerCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Entities.Walker;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class CreateWalkerCommandHandler : IRequestHandler<CreateWalkerCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<User> _userManager;
    private readonly IWalkerRepository _walkerRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;


    public CreateWalkerCommandHandler(
        ICurrentUserService currentUserService,
        UserManager<User> userManager,
        IWalkerRepository walkerRepository,
        IDogOwnerRepository dogOwnerRepository)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _walkerRepository = walkerRepository;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult> Handle(CreateWalkerCommand request, CancellationToken cancellationToken)
    {
        var dogOwner = await _dogOwnerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (dogOwner is not null)
            return Failure(WalkerErrors.UserIsAlreadyDogOwner);

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

        await _walkerRepository.SaveChangesAsync();

        return Success();
    }
}