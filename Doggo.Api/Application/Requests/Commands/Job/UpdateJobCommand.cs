namespace Doggo.Application.Requests.Commands.Job;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record UpdateJobCommand(Guid JobId ,string Comment) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateJobCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetJobRepository();

            var currentJob = await repository.GetAsync(request.JobId, cancellationToken);

            if (currentJob is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedJob = request.MapJobUpdateCommandToJob(currentJob);

            repository.Update(updatedJob);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}