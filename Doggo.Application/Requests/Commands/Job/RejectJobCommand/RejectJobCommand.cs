namespace Doggo.Application.Requests.Commands.Job.DeclineJobCommand;

using Domain.Results;
using MediatR;

public record RejectJobCommand(
    Guid JobId) : IRequest<CommonResult>;