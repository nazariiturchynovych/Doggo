namespace Doggo.Application.Requests.Commands.Job.DeleteJobCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, CommonResult>
{
    private readonly IJobRepository _jobRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IWalkerRepository _walkerRepository;

    public DeleteJobCommandHandler(
        IJobRepository jobRepository,
        ICurrentUserService currentUserService,
        IWalkerRepository walkerRepository)
    {
        _jobRepository = jobRepository;
        _currentUserService = currentUserService;
        _walkerRepository = walkerRepository;
    }

    public async Task<CommonResult> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var walker = await _walkerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (walker is null)
            return Failure(WalkerErrors.WalkerDoesNotExist);

        var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (job is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var walkerJobs = await _jobRepository.GetWalkerJobsAsync(walker.Id, cancellationToken);

        if (!walkerJobs.Any(x => x.Id == job.Id))
            return Failure(WalkerErrors.WalkerIsNotOwnerOfThisJob);

        _jobRepository.Remove(job);

        return Success();
    }
};