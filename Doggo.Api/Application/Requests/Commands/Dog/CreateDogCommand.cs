namespace Doggo.Application.Requests.Commands.Dog;

using Domain.Entities.DogOwner;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public record CreateDogCommand(
    string Name,
    double Age,
    double Weight,
    int DogOwnerId,
    string Description) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateDogCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<CommonResult> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogRepository();

            var dog = await repository.GetAsync(_currentUserService.GetUserId(), cancellationToken);

            if (dog is not null)
                return Failure("some text"); //TODO change to some const

            await repository.AddAsync(new Dog()
            {
                DogOwnerId = request.DogOwnerId,
                Age = request.Age,
                Description = request.Description,
                Name = request.Name,
                Weight = request.Weight

            });

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}