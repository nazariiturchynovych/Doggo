namespace Doggo.Application.Requests.Commands.JobRequest.DeleteJobRequestCommand;

using Base;
using Domain.Results;

public record DeleteJobRequestCommand(Guid JobRequestId) : ICommand<CommonResult>;