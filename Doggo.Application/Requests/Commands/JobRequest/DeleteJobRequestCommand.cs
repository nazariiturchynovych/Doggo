namespace Doggo.Application.Requests.Commands.JobRequest;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public record DeleteJobRequestCommand(Guid JobRequestId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteJobRequestCommand, CommonResult>
    {
        private readonly IJobRequestRepository _jobRequestRepository;

        public Handler(IJobRequestRepository jobRequestRepository)
        {
            _jobRequestRepository = jobRequestRepository;
        }

        public async Task<CommonResult> Handle(DeleteJobRequestCommand request, CancellationToken cancellationToken)
        {
            var dogOwner = await _jobRequestRepository.GetAsync(request.JobRequestId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _jobRequestRepository.Remove(dogOwner);

            return Success();
        }
    };
};