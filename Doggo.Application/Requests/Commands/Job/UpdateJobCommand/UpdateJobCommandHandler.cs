namespace Doggo.Application.Requests.Commands.Job.UpdateJobCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using Mappers;
using MediatR;

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, CommonResult>
{
    private readonly IJobRepository _jobRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IWalkerRepository _walkerRepository;

    public UpdateJobCommandHandler(
        IJobRepository jobRepository,
        ICurrentUserService currentUserService,
        IWalkerRepository walkerRepository)
    {
        _jobRepository = jobRepository;
        _currentUserService = currentUserService;
        _walkerRepository = walkerRepository;
    }

    public async Task<CommonResult> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var walker = await _walkerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (walker is null)
            return Failure(WalkerErrors.WalkerDoesNotExist);

        var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (job is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var walkerJobs = await _jobRepository.GetWalkerJobsAsync(walker.Id, cancellationToken);

        if (walkerJobs.Any(x => x.Id == job.Id))
            return Failure(WalkerErrors.WalkerIsNotOwnerOfThisJob);

        var updatedJob = request.MapJobUpdateCommandToJob(job);

        _jobRepository.Update(updatedJob);

        return Success();
    }
}