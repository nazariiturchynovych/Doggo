namespace Doggo.Application.Requests.Queries.JobRequest.GetPageOfJobRequestsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.JobRequest;

public class GetPageOfJobRequestsQueryHandler : IRequestHandler<GetPageOfJobRequestsQuery, CommonResult<PageOf<JobRequestResponse>>>
{
    private readonly IJobRequestRepository _jobRequestRepository;

    public GetPageOfJobRequestsQueryHandler(IJobRequestRepository jobRequestRepository)
    {
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult<PageOf<JobRequestResponse>>> Handle(
        GetPageOfJobRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _jobRequestRepository.GetPageOfJobRequestsAsync(
            request.DescriptionSearchTerm,
            request.SortColumn,
            request.SortOrder,
            request.PageCount,
            request.Page,
            cancellationToken);

        return Success(page.MapJobRequestCollectionToPageOJobRequestsResponse());
    }
};