namespace Doggo.Application.Requests.Queries.JobRequest.GetPageOfJobRequestsQuery;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Results;
using DTO;
using DTO.JobRequest;
using Mappers;
using MediatR;

public class GetPageOfJobRequestsQueryHandler : IRequestHandler<GetPageOfJobRequestsQuery, CommonResult<PageOfTDataDto<GetJobRequestDto>>>
{
    private readonly IJobRequestRepository _jobRequestRepository;

    public GetPageOfJobRequestsQueryHandler(IJobRequestRepository jobRequestRepository)
    {
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetJobRequestDto>>> Handle(
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

        return Success(page.MapJobRequestCollectionToPageOJobRequestsDto());
    }
};