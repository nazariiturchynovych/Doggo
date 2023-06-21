namespace Doggo.Application.Requests.Commands.JobRequest;

using Domain.Constants.ErrorConstants;
using Doggo.Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeleteJobRequestCommand(Guid JobRequestId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteJobRequestCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeleteJobRequestCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetJobRequestRepository();

            var dogOwner = await repository.GetAsync(request.JobRequestId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(dogOwner);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    };
};