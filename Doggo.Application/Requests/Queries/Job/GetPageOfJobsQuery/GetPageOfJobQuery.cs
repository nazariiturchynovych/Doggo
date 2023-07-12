namespace Doggo.Application.Requests.Queries.Job.GetPageOfJobsQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.Job;

public record GetPageOfJobsQuery(
    string? CommentSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOf<JobResponse>>>;