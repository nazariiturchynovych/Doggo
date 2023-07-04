namespace Doggo.Application.Requests.Commands.Job.ApplyJobCommand;

using Domain.Results;
using MediatR;

public record ApplyJobCommand(
    Guid JobId) : IRequest<CommonResult>;