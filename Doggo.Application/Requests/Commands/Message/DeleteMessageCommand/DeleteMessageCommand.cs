namespace Doggo.Application.Requests.Commands.Message.DeleteMessageCommand;

using Base;
using Domain.Results;

public record DeleteMessageCommand(Guid MessageId) : ICommand<CommonResult>;