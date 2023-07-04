namespace Doggo.Application.Requests.Commands.Job.CreateJobCommand;

using Domain.Results;
using MediatR;

public record CreateJobCommand(
    Guid DogId,
    Guid WalkerId,
    Guid DogOwnerId,
    Guid JobRequestId,
    string Comment,
    decimal Salary) : IRequest<CommonResult>;