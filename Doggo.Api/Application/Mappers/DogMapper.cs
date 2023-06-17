namespace Doggo.Application.Mappers;

using Domain.DTO.Dog;
using Domain.Entities.DogOwner;

public static class DogMapper
{
    public static DogDto MapDogToDogDto(this Dog dog)
    {
        return new DogDto(dog.Id, dog.Name);
    }
}