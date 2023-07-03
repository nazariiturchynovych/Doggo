namespace Doggo.Application.Requests.Commands.Dog;

using Base;
using Domain.Entities.Dog;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogRepository();

            await repository.AddAsync(new Dog()
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