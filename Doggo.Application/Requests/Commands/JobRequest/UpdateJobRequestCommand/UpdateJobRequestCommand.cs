namespace Doggo.Application.Requests.Commands.JobRequest.UpdateJobRequestCommand;

using Base;
using Domain.Results;
using Responses.JobRequest;

public record UpdateJobRequestCommand(
    Guid JobRequestId,
    int? RequiredAge,
    bool? IsPersonalIdentifierRequired,
    decimal? PaymentTo,
    string? Description,
    UpdateRequiredScheduleResponse? RequiredScheduleDto) : ICommand<CommonResult>;