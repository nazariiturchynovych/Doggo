namespace Doggo.Application.Requests.Commands.Job;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeleteJobCommand(int JobId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteJobCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetJobRepository();

            var job = await repository.GetAsync(request.JobId, cancellationToken);

            if (job is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(job);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    };
};