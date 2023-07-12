namespace Doggo.Application.Requests.Queries.Message.GetChatMessagesQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.Chat.Message;

public record GetChatMessagesQuery(
    Guid ChatId,
    int Count) : IRequest<CommonResult<List<MessageResponse>>>;