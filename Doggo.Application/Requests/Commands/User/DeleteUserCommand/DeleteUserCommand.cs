namespace Doggo.Application.Requests.Commands.User.DeleteUserCommand;

using Base;
using Domain.Results;

public record DeleteUserCommand(Guid UserId) : ICommand<CommonResult>;