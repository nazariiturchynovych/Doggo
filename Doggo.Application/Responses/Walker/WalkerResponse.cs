// ReSharper disable NotAccessedPositionalProperty.Global

namespace Doggo.Application.Responses.Walker;

public record WalkerResponse(
    Guid Id,
    Guid UserId,
    string Skills,
    string About,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);