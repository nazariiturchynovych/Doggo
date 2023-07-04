namespace Doggo.Application.Requests.Commands.JobRequest.UpdateJobRequestCommand;

using Domain.Results;
using DTO.JobRequest;
using MediatR;

public record UpdateJobRequestCommand(
    Guid JobRequestId,
    int? RequiredAge,
    bool? IsPersonalIdentifierRequired,
    decimal? PaymentTo,
    string? Description,
    UpdateRequiredScheduleDto? RequiredScheduleDto) : IRequest<CommonResult>;