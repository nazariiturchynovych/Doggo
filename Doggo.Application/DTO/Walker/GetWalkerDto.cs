// ReSharper disable NotAccessedPositionalProperty.Global

namespace Doggo.Domain.DTO.Walker;

using PossibleSchedule;

public record GetWalkerDto(
    Guid Id,
    Guid UserId,
    string Skills,
    string About,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);