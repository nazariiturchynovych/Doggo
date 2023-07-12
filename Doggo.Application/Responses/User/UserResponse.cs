// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.Responses.User;

using PersonalIdentifier;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    PersonalIdentifierResponse? PersonalIdentifier);