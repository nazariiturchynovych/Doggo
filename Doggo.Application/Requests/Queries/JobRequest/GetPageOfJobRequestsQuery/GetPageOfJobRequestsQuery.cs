namespace Doggo.Application.Requests.Queries.JobRequest.GetPageOfJobRequestsQuery;

using Domain.Results;
using DTO;
using DTO.JobRequest;
using MediatR;

public record GetPageOfJobRequestsQuery(
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetJobRequestDto>>>;