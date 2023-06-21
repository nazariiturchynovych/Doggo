// ReSharper disable NotAccessedPositionalProperty.Global

namespace Doggo.Domain.DTO.Walker;

using PossibleSchedule;

public record GetWalkerDto(
    int Id,
    string Skills,
    string About,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    IReadOnlyCollection<GetPossibleScheduleDto> Schedules);