namespace Doggo.Application.Requests.Queries.Job.GetPageOfJobsQuery;

using Domain.Results;
using DTO;
using DTO.Job;
using MediatR;

public record GetPageOfJobsQuery(
    string? CommentSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>;