namespace Doggo.Application.Requests.Commands.Chat.CreateGroupChatCommand;

using Domain.Results;
using MediatR;

public record CreateGroupChatCommand(
    string Name,
    List<Guid> UserIds) : IRequest<CommonResult>;