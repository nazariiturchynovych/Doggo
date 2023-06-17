// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.Dog;

public record GetDogDto(
    int Id,
    string Name,
    double Age,
    double? Weight,
    string Description);