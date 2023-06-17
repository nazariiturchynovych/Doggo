namespace Doggo.Application.Requests.Commands.Dog;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record UpdateDogCommand(int DogId ,double? Weight, string? Description, double? Age, string? Name) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateDogCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(UpdateDogCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogRepository();

            var currentDog = await repository.GetAsync(request.DogId, cancellationToken);

            if (currentDog is null)
                return Failure(CommonErrors.InnerError);

            var updatedDog = request.MapDogUpdateCommandToDog(currentDog);

            repository.Update(updatedDog);

            return Success();
        }
    }
}