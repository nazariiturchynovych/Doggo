namespace Doggo.Application.Requests.Commands.Dog.CreateDogCommand;

using Abstractions.Repositories;
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
        var a = await _dogRepository.GetAsync(Guid.Parse("e44a6629-f613-4480-b17c-7f669a549ffa"));

        await _dogRepository.AddAsync(
            new Dog()
            {
                Id = Guid.NewGuid(),
                DogOwnerId = request.DogOwnerId,
                Age = request.Age,
                Description = request.Description,
                Name = request.Name,
                Weight = request.Weight
            });

        // await _dogRepository.AddAsync(
        //     new Dog()
        //     {
        //         Id = Guid.NewGuid(),
        //         DogOwnerId = request.DogOwnerId,
        //         Age = request.Age,
        //         Description = request.Description,
        //         Name = request.Name,
        //         Weight = request.Weight
        //     });

        await _dogRepository.SaveChangesAsync();
        return Success();
    }
}