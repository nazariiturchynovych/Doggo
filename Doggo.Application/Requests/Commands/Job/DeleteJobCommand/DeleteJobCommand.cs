namespace Doggo.Application.Requests.Commands.Job.DeleteJobCommand;

using Base;
using Domain.Results;

public record DeleteJobCommand(Guid JobId) : ICommand<CommonResult>;