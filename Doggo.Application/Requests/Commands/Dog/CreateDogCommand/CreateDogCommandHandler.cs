namespace Doggo.Application.Requests.Commands.Dog.CreateDogCommand;

using Abstractions.Persistence.Read;
using Domain.Entities.Dog;
using Domain.Results;
using MediatR;

public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, CommonResult>
{
    private readonly IDogRepository _dogRepository;

    public CreateDogCommandHandler(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
    }

    public async Task<CommonResult> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        await _dogRepository.AddAsync(
            new Dog()
            {
                DogOwnerId = request.DogOwnerId,
                Age = request.Age,
                Description = request.Description,
                Name = request.Name,
                Weight = request.Weight
            });

        return Success();
    }
}