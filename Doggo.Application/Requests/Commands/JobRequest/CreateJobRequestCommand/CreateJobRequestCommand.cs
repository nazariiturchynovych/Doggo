namespace Doggo.Application.Requests.Commands.JobRequest.CreateJobRequestCommand;

using Base;
using Domain.Results;
using DTO.JobRequest;

public record CreateJobRequestCommand(
    Guid DogId,
    int RequiredAge,
    bool IsPersonalIdentifierRequired,
    string Description,
    decimal PaymentTo,
    GetRequiredScheduleDto GetRequiredScheduleDto
) : ICommand<CommonResult>;