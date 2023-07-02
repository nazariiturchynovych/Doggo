// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.User;

using PersonalIdentifier;

public record GetUserDto(
    Guid Id,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    GetPersonalIdentifierDto? PersonalIdentifier);