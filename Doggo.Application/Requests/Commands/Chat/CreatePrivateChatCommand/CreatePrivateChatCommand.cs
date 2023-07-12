namespace Doggo.Application.Requests.Commands.Chat.CreatePrivateChatCommand;

using Base;
using Domain.Results;

public record CreatePrivateChatCommand(
    string Name,
    Guid FirstUserId,
    Guid SecondUserId) : ICommand<CommonResult>;