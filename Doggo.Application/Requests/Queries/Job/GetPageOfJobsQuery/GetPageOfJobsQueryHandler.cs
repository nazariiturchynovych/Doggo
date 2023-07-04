namespace Doggo.Application.Requests.Queries.Job.GetPageOfJobsQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Job;
using Mappers;
using MediatR;

public class GetPageOfJobsQueryHandler : IRequestHandler<GetPageOfJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
{
    private readonly IJobRepository _jobRepository;

    public GetPageOfJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
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

        return Success(page.MapJobCollectionToPageOJobDto());
    }
};