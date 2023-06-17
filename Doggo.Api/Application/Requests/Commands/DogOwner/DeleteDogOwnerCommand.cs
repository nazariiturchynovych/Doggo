namespace Doggo.Application.Requests.Commands.DogOwner;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeleteDogOwnerCommand(int DogOwnerId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteDogOwnerCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeleteDogOwnerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogOwnerRepository();

            var dogOwner = await repository.GetAsync(request.DogOwnerId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(dogOwner);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    };
};