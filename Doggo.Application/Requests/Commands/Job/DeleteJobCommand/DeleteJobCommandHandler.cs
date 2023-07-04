namespace Doggo.Application.Requests.Commands.Job.DeleteJobCommand;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, CommonResult>
{
    private readonly IJobRepository _jobRepository;

    public DeleteJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (job is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        _jobRepository.Remove(job);

        return Success();
    }
};