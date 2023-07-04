namespace Doggo.Application.Requests.Commands.JobRequest.UpdateJobRequestCommand;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public class UpdateJobRequestCommandHandler : IRequestHandler<UpdateJobRequestCommand, CommonResult>
{
    private readonly IJobRequestRepository _jobRequestRepository;

    public UpdateJobRequestCommandHandler(IJobRequestRepository jobRequestRepository)
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