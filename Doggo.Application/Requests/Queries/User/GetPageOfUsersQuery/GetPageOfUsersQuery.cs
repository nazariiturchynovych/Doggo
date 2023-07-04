namespace Doggo.Application.Requests.Queries.User.GetPageOfUsersQuery;

using Domain.Results;
using DTO;
using DTO.User;
using MediatR;

public record GetPageOfUsersQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetUserDto>>>;