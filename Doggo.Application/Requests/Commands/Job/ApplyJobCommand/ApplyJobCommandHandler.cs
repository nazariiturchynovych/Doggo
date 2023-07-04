namespace Doggo.Application.Requests.Commands.Job.ApplyJobCommand;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public class ApplyJobCommandHandler : IRequestHandler<ApplyJobCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IJobRepository _jobRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;


    public ApplyJobCommandHandler(ICurrentUserService currentUserService, IJobRepository jobRepository, IDogOwnerRepository dogOwnerRepository)
    {
        _currentUserService = currentUserService;
        _jobRepository = jobRepository;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult> Handle(ApplyJobCommand request, CancellationToken cancellationToken)
    {

        var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (job is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var currentDogOwner = await _dogOwnerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (currentDogOwner!.Jobs.All(x => x.Id != job.Id))
            return Failure(JobErrors.CurrenDogOwnerIsNotOwnerOfThisJob);

        job.Status = JobStatus.Applied;

        _jobRepository.Update(job);

        return Success();
    }
}