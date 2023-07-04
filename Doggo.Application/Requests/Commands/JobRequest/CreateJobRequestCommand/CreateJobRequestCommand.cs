namespace Doggo.Application.Requests.Commands.JobRequest.CreateJobRequestCommand;

using Domain.Results;
using DTO.JobRequest;
using MediatR;

public record CreateJobRequestCommand(
    Guid DogId,
    Guid DogOwnerId,
    int RequiredAge,
    bool IsPersonalIdentifierRequired,
    string Description,
    decimal Salary,
    GetRequiredScheduleDto GetRequiredScheduleDto
) : IRequest<CommonResult>;