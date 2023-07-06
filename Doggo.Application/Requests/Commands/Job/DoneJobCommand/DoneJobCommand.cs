namespace Doggo.Application.Requests.Commands.Job.DoneJobCommand;

using System.Windows.Input;
using Base;
using Domain.Results;

public record DoneJobCommand(Guid JobId) : ICommand<CommonResult>;