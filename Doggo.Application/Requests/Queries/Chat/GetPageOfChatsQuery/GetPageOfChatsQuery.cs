namespace Doggo.Application.Requests.Queries.Chat.GetPageOfChatsQuery;

using Domain.Results;
using DTO;
using DTO.Chat;
using MediatR;

public record GetPageOfChatsQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<ChatDto>>>;