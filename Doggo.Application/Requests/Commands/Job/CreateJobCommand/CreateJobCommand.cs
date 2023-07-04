namespace Doggo.Application.Requests.Commands.Job.CreateJobCommand;

using Domain.Results;
using MediatR;

public record CreateJobCommand(
    Guid DogId,
    Guid DogOwnerId,
    Guid JobRequestId,
    string Comment,
    decimal Payment) : IRequest<CommonResult>;