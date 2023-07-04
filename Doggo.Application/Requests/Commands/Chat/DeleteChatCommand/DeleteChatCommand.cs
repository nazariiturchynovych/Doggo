namespace Doggo.Application.Requests.Commands.Chat.DeleteChatCommand;

using Domain.Results;
using MediatR;

public record DeleteChatCommand(Guid ChatId) : IRequest<CommonResult>;