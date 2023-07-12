namespace Doggo.Application.Requests.Commands.Message.CreateMessageCommand;

using Base;
using Domain.Results;

public record CreateMessageCommand(Guid UserId, Guid ChatId, string Value) : ICommand<CommonResult>;