namespace Doggo.Application.Requests.Queries.Chat.GetChatByIdQuery;

using Domain.Results;
using MediatR;
using Responses.Chat;

public record GetChatByIdQuery(Guid Id) : IRequest<CommonResult<ChatResponse>>;