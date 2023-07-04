namespace Doggo.Application.Requests.Commands.Chat.UpdateChatCommand;

using Domain.Results;
using MediatR;

public record UpdateChatCommand(Guid ChatId, string? Name) : IRequest<CommonResult>;