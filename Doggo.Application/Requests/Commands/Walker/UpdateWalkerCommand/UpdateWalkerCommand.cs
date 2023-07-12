namespace Doggo.Application.Requests.Commands.Walker.UpdateWalkerCommand;

using Base;
using Domain.Results;

public record UpdateWalkerCommand(Guid WalkerId, string? Skills, string? About) : ICommand<CommonResult>;