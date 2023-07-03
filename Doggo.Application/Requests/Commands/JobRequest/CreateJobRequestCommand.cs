namespace Doggo.Application.Requests.Commands.JobRequest;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedule;
using Domain.Results;
using DTO.JobRequest;
using MediatR;

public record CreateJobRequestCommand(
    Guid DogId,
    Guid DogOwnerId,
    int RequiredAge,
    bool IsPersonalIdentifierRequired,
    string Description,
    decimal Salary,
    GetRequiredScheduleDto GetRequiredScheduleDto
) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateJobRequestCommand, CommonResult>
    {
        private readonly IDogOwnerRepository _dogOwnerRepository;
        private readonly IDogRepository _dogRepository;
        private readonly IJobRequestRepository _jobRequestRepository;
        private readonly IRequiredScheduleRepository _requiredScheduleRepository;

        public Handler(
            IDogOwnerRepository dogOwnerRepository,
            IDogRepository dogRepository,
            IJobRequestRepository jobRequestRepository,
            IRequiredScheduleRepository requiredScheduleRepository)
        {
            _dogOwnerRepository = dogOwnerRepository;
            _dogRepository = dogRepository;
            _jobRequestRepository = jobRequestRepository;
            _requiredScheduleRepository = requiredScheduleRepository;
        }

        public async Task<CommonResult> Handle(CreateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var dogOwner = await _dogOwnerRepository.GetAsync(request.DogOwnerId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var dog = await _dogRepository.GetAsync(request.DogId, cancellationToken);

            if (dog is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var dogOwnerDogs = await _dogRepository.GetDogOwnerDogsAsync(
                request.DogOwnerId,
                cancellationToken: cancellationToken);

            if (dogOwnerDogs.Any(x => x.DogOwnerId != request.DogOwnerId))
                return Failure(JobRequestErrors.DogOwnerIsNotOwnerOfThisDog);

            var entityToAdd = new JobRequest
            {
                DogId = request.DogId,
                DogOwnerId = request.DogOwnerId,
                CreatedDate = DateTime.UtcNow,
                RequiredAge = request.RequiredAge,
                Description = request.Description,
                Salary = request.Salary,
                IsPersonalIdentifierRequired = request.IsPersonalIdentifierRequired,
            };

            await _jobRequestRepository.AddAsync(entityToAdd);

            await _requiredScheduleRepository.AddAsync(
                new RequiredSchedule()
                {
                    From = request.GetRequiredScheduleDto.From,
                    To = request.GetRequiredScheduleDto.To,
                    IsRegular = request.GetRequiredScheduleDto.IsRegular,
                    JobRequestId = entityToAdd.Id
                });

            return Success();
        }
    }
}