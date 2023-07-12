namespace Doggo.Application.Requests.Commands.Chat.CreateGroupChatCommand;

using Base;
using Domain.Results;

public record CreateGroupChatCommand(
    string Name,
    List<Guid> UserIds) : ICommand<CommonResult>;