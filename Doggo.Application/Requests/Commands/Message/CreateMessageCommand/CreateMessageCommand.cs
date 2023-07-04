namespace Doggo.Application.Requests.Commands.Message.CreateMessageCommand;

using Domain.Results;
using MediatR;

public record CreateMessageCommand(Guid UserId, Guid ChatId, string Value) : IRequest<CommonResult>;