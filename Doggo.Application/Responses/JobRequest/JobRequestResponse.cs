// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.Responses.JobRequest;

public record JobRequestResponse(
    Guid Id,
    bool IsPersonalIdentifierRequired,
    string Description,
    int RequiredAge,
    RequiredScheduleResponse RequiredScheduleResponse);