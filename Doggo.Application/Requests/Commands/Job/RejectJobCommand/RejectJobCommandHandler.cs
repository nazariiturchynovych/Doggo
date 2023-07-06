namespace Doggo.Application.Requests.Commands.Job.RejectJobCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using DeclineJobCommand;
using Domain.Constants.ErrorConstants;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public class RejectJobCommandHandler : IRequestHandler<RejectJobCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IJobRepository _jobRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public RejectJobCommandHandler(
        ICurrentUserService currentUserService,
        IJobRepository jobRepository,
        IDogOwnerRepository dogOwnerRepository)
    {
        _currentUserService = currentUserService;
        _jobRepository = jobRepository;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult> Handle(RejectJobCommand request, CancellationToken cancellationToken)
    {
        var currentDogOwner = await _dogOwnerRepository.GetWithJobRequestAndJobsAsyncByUserId(
            _currentUserService.GetUserId(),
            cancellationToken);

        if (currentDogOwner is null)
            return Failure(DogOwnerErrors.DogOwnerDoesNotExist);

        var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (job is null)
            return Failure(JobErrors.JobDoesNotExist);

        if (currentDogOwner.Jobs.All(x => x.Id != job.Id))
            return Failure(JobErrors.CurrenDogOwnerHasNotThisAppliedJob);


        job.Status = JobStatus.Rejected;

        _jobRepository.Update(job);

        return Success();
    }
}