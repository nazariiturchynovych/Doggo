namespace Doggo.Application.Requests.Commands.Chat.AddUsersToChatCommand;

using Domain.Results;
using MediatR;

public record AddUsersToChatCommand(Guid ChatId, ICollection<Guid> UsersId) : IRequest<CommonResult>;