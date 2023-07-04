namespace Doggo.Application.Requests.Commands.DogOwner.UpdateDogOwnerCommand;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public class UpdateDogOwnerCommandHandler : IRequestHandler<UpdateDogOwnerCommand, CommonResult>
{
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public UpdateDogOwnerCommandHandler(IDogOwnerRepository dogOwnerRepository)
    {
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult> Handle(UpdateDogOwnerCommand request, CancellationToken cancellationToken)
    {
        var currentDogOwner = await _dogOwnerRepository.GetAsync(request.DogOwnerId, cancellationToken);

        if (currentDogOwner is null)
            return Failure(CommonErrors.InnerError);

        var updatedDogOwner = request.MapDogOwnerUpdateCommandToDogOwner(currentDogOwner);

        _dogOwnerRepository.Update(updatedDogOwner);

        return Success();
    }
}