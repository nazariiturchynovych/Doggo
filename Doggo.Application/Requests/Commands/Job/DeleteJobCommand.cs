namespace Doggo.Application.Requests.Commands.Job;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public record DeleteJobCommand(Guid JobId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteJobCommand, CommonResult>
    {
        private readonly IJobRepository _jobRepository;

        public Handler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<CommonResult> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(request.JobId, cancellationToken);

            if (job is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _jobRepository.Remove(job);

            return Success();
        }
    };
};