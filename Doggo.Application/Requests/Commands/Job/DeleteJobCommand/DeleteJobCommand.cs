namespace Doggo.Application.Requests.Commands.Job.DeleteJobCommand;

using Domain.Results;
using MediatR;

public record DeleteJobCommand(Guid JobId) : IRequest<CommonResult>;