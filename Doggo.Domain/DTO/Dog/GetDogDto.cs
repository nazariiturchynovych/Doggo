// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.Dog;

public record GetDogDto(
    Guid Id,
    Guid DogOwnerId,
    string Name,
    double Age,
    double? Weight,
    string Description);