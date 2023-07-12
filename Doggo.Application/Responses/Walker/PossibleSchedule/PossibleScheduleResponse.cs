// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.Responses.Walker.PossibleSchedule;

public record PossibleScheduleResponse(
    Guid Id,
    DateTime From,
    DateTime To);