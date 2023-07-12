namespace Doggo.Application.Requests.Commands.Job.DoneJobCommand;

using Base;
using Domain.Results;

public record DoneJobCommand(Guid JobId) : ICommand<CommonResult>;