namespace Doggo.Application.Requests.Commands.Job;

using Domain.Entities.Job;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record CreateJobCommand(
    Guid DogId,
    Guid WalkerId,
    Guid DogOwnerId,
    Guid JobRequestId,
    string Comment,
    decimal Salary) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateJobCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetJobRepository();

            await repository.AddAsync(new Job()
            {
                Salary = request.Salary,
                DogId = request.DogId,
                WalkerId = request.WalkerId,
                DogOwnerId = request.DogOwnerId,
                JobRequestId = request.JobRequestId,
                CreatedDate = DateTime.UtcNow,
                Comment = request.Comment,
                Status = JobStatus.Pending,
            });

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}