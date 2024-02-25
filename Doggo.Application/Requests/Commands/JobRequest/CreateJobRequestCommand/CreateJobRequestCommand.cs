namespace Doggo.Application.Requests.Commands.JobRequest.CreateJobRequestCommand;

using Base;
using Domain.Results;
using Responses.JobRequest;

public record CreateJobRequestCommand(
    Guid DogId,
    int RequiredAge,
    bool IsPersonalIdentifierRequired,
    string Description,
    decimal PaymentTo,
    RequiredScheduleResponse RequiredScheduleResponse
) : ICommand<CommonResult>; //TODO change required schedule frm which time and how much time