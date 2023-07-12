namespace Doggo.Application.Requests.Commands.Message.UpdateMessageCommand;

using Base;
using Domain.Results;

public record UpdateMessageCommand(Guid MessageId ,string Value) : ICommand<CommonResult>;