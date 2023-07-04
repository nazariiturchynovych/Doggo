namespace Doggo.Application.Requests.Commands.Dog.UpdateDogCommand;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public class UpdateDogCommandHandler : IRequestHandler<UpdateDogCommand, CommonResult>
{
    private readonly IDogRepository _dogRepository;


    public UpdateDogCommandHandler(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
    }

    public async Task<CommonResult> Handle(UpdateDogCommand request, CancellationToken cancellationToken)
    {
        var dog = await _dogRepository.GetAsync(request.DogId, cancellationToken);

        if (dog is null)
            return Failure(CommonErrors.InnerError);

        var updatedDog = request.MapDogUpdateCommandToDog(dog);

        _dogRepository.Update(updatedDog);

        return Success();
    }
}