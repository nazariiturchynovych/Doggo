namespace Doggo.Application.Requests.Commands.Job.UpdateJobCommand;

using Domain.Results;
using MediatR;

public record UpdateJobCommand(Guid JobId, string Comment, decimal? Payment) : IRequest<CommonResult>;