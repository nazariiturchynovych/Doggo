namespace Doggo.Application.Requests.Commands.JobRequest;

using Domain.Constants.ErrorConstants;
using Domain.DTO.JobRequest;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(UpdateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetJobRequestRepository();

            var currentJobRequest = await repository.GetAsync(request.JobRequestId, cancellationToken);

            if (currentJobRequest is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedJobRequest = request.MapUpdateJobRequestCommandToJobRequest(currentJobRequest);

            repository.Update(updatedJobRequest);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}