namespace Doggo.Application.Requests.Commands.Walker.DeleteWalkerCommand;

using Base;
using Domain.Results;

public record DeleteWalkerCommand(Guid WalkerId) : ICommand<CommonResult>;