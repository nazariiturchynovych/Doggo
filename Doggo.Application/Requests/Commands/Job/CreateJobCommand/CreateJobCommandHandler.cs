namespace Doggo.Application.Requests.Commands.Job.CreateJobCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Job;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, CommonResult>
{
    private readonly IJobRepository _jobRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IWalkerRepository _walkerRepository;
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public CreateJobCommandHandler(
        IJobRepository jobRepository,
        ICurrentUserService currentUserService,
        IWalkerRepository walkerRepository,
        IJobRequestRepository jobRequestRepository,
        IDogOwnerRepository dogOwnerRepository)
    {
        _jobRepository = jobRepository;
        _currentUserService = currentUserService;
        _walkerRepository = walkerRepository;
        _jobRequestRepository = jobRequestRepository;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var walker = await _walkerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (walker is null)
            return Failure(WalkerErrors.WalkerDoesNotExist);

        var dogOwner = await _dogOwnerRepository.GetAsync(request.DogOwnerId, cancellationToken);

        if (dogOwner is null)
            return Failure(DogOwnerErrors.DogOwnerDoesNotExist);

        if (dogOwner.UserId == _currentUserService.GetUserId())
            return Failure(WalkerErrors.WalkerCanNotApplyJobForHisOwnJobRequest);

        var jobRequest = await _jobRequestRepository.GetAsync(request.JobRequestId, cancellationToken);

        if (jobRequest is null)
            return Failure(JobRequestErrors.JobRequestDoesNotExist);

        if (jobRequest.HasAcceptedJob)
            return Failure(JobRequestErrors.JobRequestAlreadyHAveAcceptedJob);

        if (request.Payment > jobRequest.PaymentTo)
            return Failure(JobRequestErrors.JobRequestPaymentIsLessThanRequired);

        await _jobRepository.AddAsync(
            new Job()
            {
                Salary = request.Payment,
                DogId = request.DogId,
                WalkerId = walker.Id,
                DogOwnerId = request.DogOwnerId,
                JobRequestId = request.JobRequestId,
                CreatedDate = DateTime.UtcNow,
                Comment = request.Comment,
                Status = JobStatus.Applied,
            });

        return Success();
    }
}