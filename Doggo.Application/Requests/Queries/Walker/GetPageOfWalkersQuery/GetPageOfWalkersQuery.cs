namespace Doggo.Application.Requests.Queries.Walker.GetPageOfWalkersQuery;

using Domain.Results;
using DTO;
using DTO.Walker;
using MediatR;

public record GetPageOfWalkersQuery(
    string? NameSearchTerm,
    string? SkillSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount
) : IRequest<CommonResult<PageOfTDataDto<GetWalkerDto>>>;