namespace Doggo.Application.Requests.Commands.Message.DeleteMessageCommand;

using Domain.Results;
using MediatR;

public record DeleteMessageCommand(Guid MessageId) : IRequest<CommonResult>;