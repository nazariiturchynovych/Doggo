namespace Doggo.Application.Requests.Commands.Job.UpdateJobCommand;

using Base;
using Domain.Results;

public record UpdateJobCommand(Guid JobId, string Comment, decimal? Payment) : ICommand<CommonResult>;