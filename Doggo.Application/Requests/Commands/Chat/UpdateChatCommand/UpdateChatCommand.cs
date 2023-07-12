namespace Doggo.Application.Requests.Commands.Chat.UpdateChatCommand;

using Base;
using Domain.Results;

public record UpdateChatCommand(Guid ChatId, string? Name) : ICommand<CommonResult>;