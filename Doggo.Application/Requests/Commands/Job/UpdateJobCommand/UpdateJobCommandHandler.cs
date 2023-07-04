namespace Doggo.Application.Requests.Commands.Job.UpdateJobCommand;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, CommonResult>
{
    private readonly IJobRepository _jobRepository;

    public UpdateJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var currentJob = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (currentJob is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var updatedJob = request.MapJobUpdateCommandToJob(currentJob);

        _jobRepository.Update(updatedJob);

        return Success();
    }
}