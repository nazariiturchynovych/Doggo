namespace Doggo.Application.Requests.Commands.Job.CreateJobCommand;

using Abstractions.Persistence.Read;
using Domain.Entities.Job;
using Domain.Enums;
using Domain.Results;
using MediatR;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, CommonResult>
{
    private readonly IJobRepository _jobRepository;

    public CreateJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        await _jobRepository.AddAsync(
            new Job()
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

        return Success();
    }
}