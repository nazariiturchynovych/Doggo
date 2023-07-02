namespace Doggo.Application.Requests.Commands.JobRequest;

using Domain.Constants.ErrorConstants;
using Domain.DTO.JobRequest;
using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedules;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(CreateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var dogOwnerRepository = _unitOfWork.GetDogOwnerRepository();

            var dogOwner = await dogOwnerRepository.GetAsync(request.DogOwnerId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var dogRepository = _unitOfWork.GetDogRepository();

            var dog = await dogRepository.GetAsync(request.DogId, cancellationToken);

            if (dog is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var dogOwnerDogs = await dogRepository.GetDogOwnerDogsAsync(request.DogOwnerId, cancellationToken: cancellationToken);

            if (dogOwnerDogs.Any(x => x.DogOwnerId != request.DogOwnerId))
                return Failure(JobRequestErrors.DogOwnerIsNotOwnerOfThisDog);

            var jobRequestRepository = _unitOfWork.GetJobRequestRepository();

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

            await jobRequestRepository.AddAsync(entityToAdd);

            var requiredScheduleRepository = _unitOfWork.GetRequiredScheduleRepository();

            await requiredScheduleRepository.AddAsync(
                new RequiredSchedule()
                {
                    From = request.GetRequiredScheduleDto.From,
                    To = request.GetRequiredScheduleDto.To,
                    IsRegular = request.GetRequiredScheduleDto.IsRegular,
                    JobRequestId = entityToAdd.Id
                }); //TODO entity entry can be null

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}