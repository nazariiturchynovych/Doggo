namespace Doggo.Application.Requests.Queries.Job.GetWalkerJobsQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Job;
using Mappers;
using MediatR;

public class GetWalkerJobsQueryHandler : IRequestHandler<GetWalkerJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
{
    private readonly IJobRepository _jobRepository;

    public GetWalkerJobsQueryHandler(IJobRepository jobRepository)
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