namespace Doggo.Application.Requests.Commands.Job.DoneJobCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public class DoneJobCommandHandler : IRequestHandler<DoneJobCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IJobRepository _jobRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;
    private readonly IJobRequestRepository _jobRequestRepository;

    public DoneJobCommandHandler(ICurrentUserService currentUserService, IJobRepository jobRepository, IDogOwnerRepository dogOwnerRepository,
        IJobRequestRepository jobRequestRepository)
    {
        _currentUserService = currentUserService;
        _jobRepository = jobRepository;
        _dogOwnerRepository = dogOwnerRepository;
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult> Handle(DoneJobCommand request, CancellationToken cancellationToken)
    {
        var currentDogOwner = await _dogOwnerRepository.GetWithJobRequestAndJobsAsyncByUserId(
            _currentUserService.GetUserId(),
            cancellationToken);

        if (currentDogOwner is null)
            return Failure(DogOwnerErrors.DogOwnerDoesNotExist);

        var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (job is null)
            return Failure(JobErrors.JobDoesNotExist);

        var jobRequest = await _jobRequestRepository.GetJobRequestWithJobsAsync(job.JobRequestId);

        if (jobRequest is null)
            return Failure(JobRequestErrors.JobRequestDoesNotExist);

        if (!currentDogOwner.Jobs.Any(x => x.Id == job.Id))
            return Failure(JobErrors.CurrenDogOwnerHasNotThisAppliedJob);

        job.IsDone = true;

        _jobRepository.Update(job);

        _jobRequestRepository.Remove(jobRequest);

        return Success();
    }
}