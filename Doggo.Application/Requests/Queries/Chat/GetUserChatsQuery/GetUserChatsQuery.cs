namespace Doggo.Application.Requests.Queries.Chat.GetUserChatsQuery;

using Domain.Results;
using MediatR;
using Responses.Chat;

public record GetUserChatsQuery(Guid ChatOwnerId) : IRequest<CommonResult<List<ChatResponse>>>;