namespace Doggo.Application.Requests.Queries.Walker.GetPageOfWalkersQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.Walker;

public record GetPageOfWalkersQuery(
    string? NameSearchTerm,
    string? SkillSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount
) : IRequest<CommonResult<PageOf<WalkerResponse>>>;