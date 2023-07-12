// ReSharper disable NotAccessedPositionalProperty.Global

namespace Doggo.Application.Responses.DogOwner;

public record DogOwnerResponse(
    Guid Id,
    Guid UserId,
    string Address,
    string District,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);