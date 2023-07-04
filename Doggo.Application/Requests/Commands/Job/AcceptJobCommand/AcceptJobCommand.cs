namespace Doggo.Application.Requests.Commands.Job.ApplyJobCommand;

using Domain.Results;
using MediatR;

public record AcceptJobCommand(
    Guid JobId) : IRequest<CommonResult>;