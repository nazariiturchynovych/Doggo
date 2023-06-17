// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.DogOwner;

using Dog;

public record GetDogOwnerDto(int Id,string Address, string District, IReadOnlyCollection<DogDto> Dogs);