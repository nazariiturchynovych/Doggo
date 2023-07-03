namespace Doggo.Application.Requests.Commands.JobRequest;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.JobRequest;
using Mappers;
using MediatR;

public record UpdateJobRequestCommand(
    Guid JobRequestId,
    int? RequiredAge,
    bool? IsPersonalIdentifierRequired,
    decimal? Salary,
    string? Description,
    UpdateRequiredScheduleDto? RequiredScheduleDto) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateJobRequestCommand, CommonResult>
    {
        private readonly IJobRequestRepository _jobRequestRepository;

        public Handler(IJobRequestRepository jobRequestRepository)
        {
            _jobRequestRepository = jobRequestRepository;
        }

        public async Task<CommonResult> Handle(UpdateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var jobRequest = await _jobRequestRepository.GetAsync(request.JobRequestId, cancellationToken);

            if (jobRequest is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedJobRequest = request.MapUpdateJobRequestCommandToJobRequest(jobRequest);

            _jobRequestRepository.Update(updatedJobRequest);

            return Success();
        }
    }
}