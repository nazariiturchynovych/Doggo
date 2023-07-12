namespace Doggo.Application.Requests.Commands.Job.CreateAndApplyJobCommand;

using Base;
using Domain.Results;

public record CreateAndApplyJobCommand(
    Guid DogId,
    Guid DogOwnerId,
    Guid JobRequestId,
    string Comment,
    decimal Payment) : ICommand<CommonResult>;