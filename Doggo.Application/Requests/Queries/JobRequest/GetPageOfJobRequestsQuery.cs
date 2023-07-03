namespace Doggo.Application.Requests.Queries.JobRequest;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.JobRequest;
using Mappers;
using MediatR;

public record GetPageOfJobRequestsQuery(
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetJobRequestDto>>>
{
    public class Handler : IRequestHandler<GetPageOfJobRequestsQuery, CommonResult<PageOfTDataDto<GetJobRequestDto>>>
    {
        private readonly IJobRequestRepository _jobRequestRepository;

        public Handler(IJobRequestRepository jobRequestRepository)
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
};