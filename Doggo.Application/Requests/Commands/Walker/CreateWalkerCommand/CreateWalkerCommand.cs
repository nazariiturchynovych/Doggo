namespace Doggo.Application.Requests.Commands.Walker.CreateWalkerCommand;

using Base;
using Domain.Results;

public record CreateWalkerCommand(string Skills, string About) : ICommand<CommonResult>;