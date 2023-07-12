namespace Doggo.Application.Requests.Commands.Job.RejectJobCommand;

using Base;
using Domain.Results;

public record RejectJobCommand(
    Guid JobId) : ICommand<CommonResult>;