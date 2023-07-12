namespace Doggo.Application.Requests.Queries.Job.GetPageOfJobsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.Job;

public class GetPageOfJobsQueryHandler : IRequestHandler<GetPageOfJobsQuery, CommonResult<PageOf<JobResponse>>>
{
    private readonly IJobRepository _jobRepository;

    public GetPageOfJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<PageOf<JobResponse>>> Handle(
        GetPageOfJobsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _jobRepository.GetPageOfJobsAsync(
            request.CommentSearchTerm,
            request.SortColumn,
            request.SortOrder,
            request.PageCount,
            request.Page,
            cancellationToken);

        return Success(page.MapJobCollectionToPageOfJobResponse());
    }
};