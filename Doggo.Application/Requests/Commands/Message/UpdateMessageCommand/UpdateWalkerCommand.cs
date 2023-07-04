namespace Doggo.Application.Requests.Commands.Message.UpdateMessageCommand;

using Domain.Results;
using MediatR;

public record UpdateMessageCommand(Guid MessageId ,string Value) : IRequest<CommonResult>;