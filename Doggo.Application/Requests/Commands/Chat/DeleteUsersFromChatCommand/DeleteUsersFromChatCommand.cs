namespace Doggo.Application.Requests.Commands.Chat.DeleteUsersFromChatCommand;

using Domain.Results;
using MediatR;

public record DeleteUsersFromChatCommand(Guid ChatId, ICollection<Guid> UsersId) : IRequest<CommonResult>;