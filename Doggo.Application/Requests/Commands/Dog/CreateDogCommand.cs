namespace Doggo.Application.Requests.Commands.Dog;

using Abstractions.Persistence.Read;
using Base;
using Domain.Entities.Dog;
using Domain.Results;
using MediatR;

public record CreateDogCommand(
    string Name,
    double Age,
    double? Weight,
    Guid DogOwnerId,
    string Description) : ICommand<CommonResult>
{
    public class Handler : IRequestHandler<CreateDogCommand, CommonResult>
    {
        private readonly IDogRepository _dogRepository;

        public Handler(IDogRepository dogRepository)
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
}