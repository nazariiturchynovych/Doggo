namespace Doggo.Application.Requests.Commands.Job.CreateAndApplyJobCommand;

using Domain.Results;
using MediatR;

public record CreateAndApplyJobCommand(
    Guid DogId,
    Guid DogOwnerId,
    Guid JobRequestId,
    string Comment,
    decimal Payment) : IRequest<CommonResult>;