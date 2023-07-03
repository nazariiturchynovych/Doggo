// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.DTO.JobRequest;

public record GetJobRequestDto(
    Guid Id,
    bool IsPersonalIdentifierRequired,
    string Description,
    int RequiredAge,
    GetRequiredScheduleDto RequiredScheduleDto);