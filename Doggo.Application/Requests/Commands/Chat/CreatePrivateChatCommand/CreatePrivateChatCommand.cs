namespace Doggo.Application.Requests.Commands.Chat.CreatePrivateChatCommand;

using Domain.Results;
using MediatR;

public record CreatePrivateChatCommand(
    string Name,
    Guid FirstUserId,
    Guid SecondUserId) : IRequest<CommonResult>;