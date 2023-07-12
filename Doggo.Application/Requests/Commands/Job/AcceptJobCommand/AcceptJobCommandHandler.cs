namespace Doggo.Application.Requests.Commands.Job.AcceptJobCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Enums;
using Domain.Results;
using MediatR;

public class AcceptJobCommandHandler : IRequestHandler<AcceptJobCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IJobRepository _jobRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;
    private readonly IJobRequestRepository _jobRequestRepository;


    public AcceptJobCommandHandler(
        ICurrentUserService currentUserService,
        IJobRepository jobRepository,
        IDogOwnerRepository dogOwnerRepository,
        IJobRequestRepository jobRequestRepository)
    {
        _currentUserService = currentUserService;
        _jobRepository = jobRepository;
        _dogOwnerRepository = dogOwnerRepository;
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult> Handle(AcceptJobCommand request, CancellationToken cancellationToken)
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

        job.Status = JobStatus.Accepted;

        _jobRepository.Update(job);

        jobRequest.HasAcceptedJob = true;

        _jobRequestRepository.Update(jobRequest);

        var listOfJobs = jobRequest.Jobs.ToList();

        listOfJobs.Remove(job);

        _jobRepository.RemoveRange(listOfJobs);

        return Success();
    }
}