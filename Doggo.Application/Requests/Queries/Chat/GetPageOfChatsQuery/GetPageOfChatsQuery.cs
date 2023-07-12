namespace Doggo.Application.Requests.Queries.Chat.GetPageOfChatsQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.Chat;

public record GetPageOfChatsQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOf<ChatResponse>>>;