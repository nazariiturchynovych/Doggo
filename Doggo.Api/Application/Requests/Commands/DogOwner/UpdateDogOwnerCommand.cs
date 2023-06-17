namespace Doggo.Application.Requests.Commands.DogOwner;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record UpdateDogOwnerCommand(int DogOwnerId ,string? Address, string? District) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateDogOwnerCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(UpdateDogOwnerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogOwnerRepository();

            var currentDogOwner = await repository.GetAsync(request.DogOwnerId, cancellationToken);

            if (currentDogOwner is null)
                return Failure(CommonErrors.InnerError);

            var updatedDogOwner = request.MapDogOwnerUpdateCommandToDogOwner(currentDogOwner);

            repository.Update(updatedDogOwner);

            return Success();
        }
    }
}