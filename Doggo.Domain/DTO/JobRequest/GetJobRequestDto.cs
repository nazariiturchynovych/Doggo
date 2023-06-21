// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.JobRequest;

public record GetJobRequestDto(
    Guid Id,
    bool IsPersonalIdentifierRequired,
    string Description,
    int RequiredAge,
    GetRequiredScheduleDto RequiredScheduleDto);