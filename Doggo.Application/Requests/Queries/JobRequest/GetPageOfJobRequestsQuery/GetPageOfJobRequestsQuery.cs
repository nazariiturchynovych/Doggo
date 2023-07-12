namespace Doggo.Application.Requests.Queries.JobRequest.GetPageOfJobRequestsQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.JobRequest;

public record GetPageOfJobRequestsQuery(
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOf<JobRequestResponse>>>;