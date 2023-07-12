namespace Doggo.Application.Requests.Commands.Chat.AddUsersToChatCommand;

using Base;
using Domain.Results;

public record AddUsersToChatCommand(Guid ChatId, ICollection<Guid> UsersId) : ICommand<CommonResult>;