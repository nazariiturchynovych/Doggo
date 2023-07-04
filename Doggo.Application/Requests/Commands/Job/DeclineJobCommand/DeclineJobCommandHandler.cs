namespace Doggo.Application.Requests.Commands.Job.DeclineJobCommand;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public class DeclineJobCommandHandler : IRequestHandler<DeclineJobCommand, CommonResult>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IJobRepository _jobRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public DeclineJobCommandHandler(
        ICurrentUserService currentUserService,
        IJobRepository jobRepository,
        IDogOwnerRepository dogOwnerRepository)
    {
        _currentUserService = currentUserService;
        _jobRepository = jobRepository;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult> Handle(DeclineJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

        if (job is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var currentDogOwner = await _dogOwnerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (currentDogOwner!.Jobs.All(x => x.Id != job.Id))
            return Failure(JobErrors.CurrenDogOwnerIsNotOwnerOfThisJob);

        job.Status = JobStatus.Declined;

        _jobRepository.Update(job);

        return Success();
    }
}