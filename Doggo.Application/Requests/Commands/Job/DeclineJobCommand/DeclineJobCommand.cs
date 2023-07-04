namespace Doggo.Application.Requests.Commands.Job.DeclineJobCommand;

using Domain.Results;
using MediatR;

public record DeclineJobCommand(
    Guid JobId) : IRequest<CommonResult>;