namespace Doggo.Application.Requests.Commands.Dog;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeleteDogCommand(int DogId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteDogCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogRepository();

            var dogOwner = await repository.GetAsync(request.DogId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(dogOwner);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    };
};