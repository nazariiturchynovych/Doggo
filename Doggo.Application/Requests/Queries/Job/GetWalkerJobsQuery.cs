namespace Doggo.Application.Requests.Queries.Job;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Job;
using Mappers;
using MediatR;

public record GetWalkerJobsQuery(Guid WalkerId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
{
    public class Handler : IRequestHandler<GetWalkerJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
    {
        private readonly IJobRepository _jobRepository;

        public Handler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
            GetWalkerJobsQuery request,
            CancellationToken cancellationToken)
        {

            var page = await _jobRepository.GetWalkerJobsAsync(request.WalkerId, cancellationToken);

            return Success(page.MapJobCollectionToPageOJobDto());
        }
    }
}