namespace Doggo.Application.Requests.Commands.Chat.DeleteUsersFromChatCommand;

using Base;
using Domain.Results;

public record DeleteUsersFromChatCommand(Guid ChatId, ICollection<Guid> UsersId) : ICommand<CommonResult>;