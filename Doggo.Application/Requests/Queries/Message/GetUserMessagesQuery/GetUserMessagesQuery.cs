namespace Doggo.Application.Requests.Queries.Message.GetUserMessagesQuery;

using Domain.Results;
using MediatR;
using Responses.Chat.Message;

public record GetUserMessagesQuery(Guid UserId) : IRequest<CommonResult<List<MessageResponse>>>;