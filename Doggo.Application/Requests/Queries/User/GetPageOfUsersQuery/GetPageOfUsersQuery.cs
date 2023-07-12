namespace Doggo.Application.Requests.Queries.User.GetPageOfUsersQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.User;

public record GetPageOfUsersQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOf<UserResponse>>>;