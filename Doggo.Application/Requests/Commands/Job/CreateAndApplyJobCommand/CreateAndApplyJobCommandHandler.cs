namespace Doggo.Application.Requests.Commands.Job.CreateAndApplyJobCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Job;
using Domain.Enums;
using Domain.Results;
using MediatR;

public class CreateAndApplyJobCommandHandler : IRequestHandler<CreateAndApplyJobCommand, CommonResult> //TODO go through all queries and delete all unnecessary checks
{
    private readonly IJobRepository _jobRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IWalkerRepository _walkerRepository;
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public CreateAndApplyJobCommandHandler(
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

    public async Task<CommonResult> Handle(CreateAndApplyJobCommand request, CancellationToken cancellationToken)
    {
        var walker = await _walkerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (walker is null)
            return Failure(WalkerErrors.WalkerDoesNotExist);

        var jobRequest = await _jobRequestRepository.GetAsync(request.JobRequestId, cancellationToken);

        if (jobRequest is null)
            return Failure(JobRequestErrors.JobRequestDoesNotExist);

        if (jobRequest.HasAcceptedJob)
            return Failure(JobRequestErrors.JobRequestAlreadyHasAcceptedJob);

        var dogOwnerJobRequests = await _jobRequestRepository.GetDogOwnerJobRequests(jobRequest.DogOwnerId, cancellationToken);

        if (dogOwnerJobRequests.All(x => x.Id != jobRequest.Id))
            return Failure(JobRequestErrors.CurrentDogOwnerIsNotOwnerOfThisJobRequest);

        var dogOwner = await _dogOwnerRepository.GetAsync(jobRequest.DogOwnerId, cancellationToken);


        if (dogOwner.UserId == _currentUserService.GetUserId())
            return Failure(WalkerErrors.WalkerCanNotApplyJobForHisOwnJobRequest);


        if (request.Payment > jobRequest.PaymentTo)
            return Failure(JobRequestErrors.JobRequestPaymentIsLessThanRequired);

        await _jobRepository.AddAsync(
            new Job
            {
                Payment = request.Payment,
                DogId = jobRequest.DogId,
                WalkerId = walker.Id,
                DogOwnerId = jobRequest.DogOwnerId,
                JobRequestId = request.JobRequestId,
                CreatedDate = DateTime.UtcNow,
                Comment = request.Comment,
                Status = JobStatus.Applied,
            });

        await _jobRepository.SaveChangesAsync();

        return Success();
    }
}