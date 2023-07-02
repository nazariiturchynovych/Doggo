// ReSharper disable NotAccessedPositionalProperty.Global

namespace Doggo.Domain.DTO.DogOwner;

using Dog;
using Job;
using JobRequest;

public record GetDogOwnerDto(
    Guid Id,
    Guid UserId,
    string Address,
    string District,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);