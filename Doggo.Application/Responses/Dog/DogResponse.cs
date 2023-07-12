// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.Responses.Dog;

public record DogResponse(
    Guid Id,
    Guid DogOwnerId,
    string Name,
    double Age,
    double? Weight,
    string Description);