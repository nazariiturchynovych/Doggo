namespace Doggo.Application.Requests.Commands.JobRequest.CreateJobRequestCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedule;
using Domain.Results;
using MediatR;

public class CreateJobRequestCommandHandler : IRequestHandler<CreateJobRequestCommand, CommonResult>
{
    private readonly IDogOwnerRepository _dogOwnerRepository;
    private readonly IDogRepository _dogRepository;
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly IRequiredScheduleRepository _requiredScheduleRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateJobRequestCommandHandler(
        IDogOwnerRepository dogOwnerRepository,
        IDogRepository dogRepository,
        IJobRequestRepository jobRequestRepository,
        IRequiredScheduleRepository requiredScheduleRepository,
        ICurrentUserService currentUserService)
    {
        _dogOwnerRepository = dogOwnerRepository;
        _dogRepository = dogRepository;
        _jobRequestRepository = jobRequestRepository;
        _requiredScheduleRepository = requiredScheduleRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CommonResult> Handle(CreateJobRequestCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        var dogOwner = await _dogOwnerRepository.GetByUserIdAsync(userId, cancellationToken);

        if (dogOwner is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var dog = await _dogRepository.GetAsync(request.DogId, cancellationToken);

        if (dog is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var dogOwnerDogs = await _dogRepository.GetDogOwnerDogsAsync(
            dogOwner.Id,
            cancellationToken: cancellationToken);

        if (dogOwnerDogs.Any(x => x.Id != dog.Id))
            return Failure(DogErrors.DogOwnerIsNotOwnerOfThisDog);

        var entityToAdd = new JobRequest
        {
            DogId = request.DogId,
            DogOwnerId = dogOwner.Id,
            CreatedDate = DateTime.UtcNow,
            RequiredAge = request.RequiredAge,
            Description = request.Description,
            PaymentTo = request.PaymentTo,
            HasAcceptedJob = false,
            IsPersonalIdentifierRequired = request.IsPersonalIdentifierRequired,
        };

        await _jobRequestRepository.AddAsync(entityToAdd);

        await _requiredScheduleRepository.AddAsync(
            new RequiredSchedule()
            {
                From = request.RequiredScheduleResponse.From,
                To = request.RequiredScheduleResponse.To,
                JobRequestId = entityToAdd.Id
            });

        return Success();
    }
}