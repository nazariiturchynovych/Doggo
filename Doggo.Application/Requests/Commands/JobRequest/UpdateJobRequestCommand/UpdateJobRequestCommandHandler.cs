namespace Doggo.Application.Requests.Commands.JobRequest.UpdateJobRequestCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public class UpdateJobRequestCommandHandler : IRequestHandler<UpdateJobRequestCommand, CommonResult>
{
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public UpdateJobRequestCommandHandler(
        IJobRequestRepository jobRequestRepository,
        ICurrentUserService currentUserService,
        IDogOwnerRepository dogOwnerRepository)
    {
        _jobRequestRepository = jobRequestRepository;
        _currentUserService = currentUserService;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult> Handle(UpdateJobRequestCommand request, CancellationToken cancellationToken)
    {
        var dogOwner = await _dogOwnerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

        if (dogOwner is null)
            return Failure(DogOwnerErrors.DogOwnerDoesNotExist);

        var dogOwnerJobRequests = await _jobRequestRepository.GetDogOwnerJobRequests(dogOwner.Id, cancellationToken);

        if (dogOwnerJobRequests.Any(x => x.Id == request.JobRequestId))
            return Failure(JobRequestErrors.CurrentDogOwnerIsNotOwnerOfThisJobRequest);

        var jobRequest = await _jobRequestRepository.GetAsync(request.JobRequestId, cancellationToken);

        if (jobRequest is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        if (jobRequest.ValidFrom < DateTime.UtcNow)
            Failure(JobRequestErrors.JobRequestCanBeChangedOnlyForTenMinutesAfterItWasCreated);

        var updatedJobRequest = request.MapUpdateJobRequestCommandToJobRequest(jobRequest);

        _jobRequestRepository.Update(updatedJobRequest);

        return Success();
    }
}