namespace Doggo.Application.Requests.Commands.Job.CreateAndApplyJobCommand;

using Base;
using Domain.Results;

public record CreateAndApplyJobCommand(
    Guid JobRequestId,
    string Comment,
    decimal Payment) : ICommand<CommonResult>;