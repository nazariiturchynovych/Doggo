namespace Doggo.Application.Requests.Commands.Chat.DeleteChatCommand;

using Base;
using Domain.Results;

public record DeleteChatCommand(Guid ChatId) : ICommand<CommonResult>;