// ReSharper disable NotAccessedPositionalProperty.Global

namespace Doggo.Application.DTO.DogOwner;

public record GetDogOwnerDto(
    Guid Id,
    Guid UserId,
    string Address,
    string District,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);