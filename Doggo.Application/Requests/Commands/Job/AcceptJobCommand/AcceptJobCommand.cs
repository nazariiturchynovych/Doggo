namespace Doggo.Application.Requests.Commands.Job.AcceptJobCommand;

using Base;
using Domain.Results;

public record AcceptJobCommand(
    Guid JobId) : ICommand<CommonResult>;