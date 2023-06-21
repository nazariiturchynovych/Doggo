namespace Doggo.Application.Requests.Commands.JobRequest;

using Domain.Constants.ErrorConstants;
using Domain.DTO.JobRequest;
using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedules;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
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
        private readonly ICurrentUserService _currentUserService;

        public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<CommonResult> Handle(CreateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var jobRequestRepository = _unitOfWork.GetJobRequestRepository();

            var jobRequest = await jobRequestRepository.GetAsync(_currentUserService.GetUserId(), cancellationToken);

            if (jobRequest is not null)
                return Failure(CommonErrors.EntityAlreadyExist);

            var entityToAdd = new JobRequest
            {
                DogId = request.DogId,
                DogOwnerId = request.DogOwnerId,
                CreatedDate = DateTime.Now,
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
                    DayOfWeek = request.GetRequiredScheduleDto.DayOfWeek,
                    IsRegular = request.GetRequiredScheduleDto.IsRegular,
                    JobRequestId = entityToAdd.Id
                }); //TODO entity entry can be null

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}