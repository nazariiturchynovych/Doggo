namespace Doggo.Application.Requests.Queries.Job.GetWalkerJobsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Job;

public class GetWalkerJobsQueryHandler : IRequestHandler<GetWalkerJobsQuery, CommonResult<List<JobResponse>>>
{
    private readonly IJobRepository _jobRepository;

    public GetWalkerJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<List<JobResponse>>> Handle(
        GetWalkerJobsQuery request,
        CancellationToken cancellationToken)
    {

        var page = await _jobRepository.GetWalkerJobsAsync(request.WalkerId, cancellationToken);

        return Success(page.MapJobCollectionToListJobResponse());
    }
}