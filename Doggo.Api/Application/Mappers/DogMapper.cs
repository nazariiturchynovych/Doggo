namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.Dog;
using Domain.Entities.DogOwner;
using Requests.Commands.Dog;

public static class DogMapper
{
    public static DogDto MapDogToDogDto(this Dog dog)
    {
        return new DogDto(dog.Id, dog.Name);
    }

    public static Dog MapDogUpdateCommandToDog(this UpdateDogCommand command, Dog dog)
    {
        dog.Age = command.Age ?? dog.Age;
        dog.Weight = command.Weight ?? dog.Weight;
        dog.Description = command.Description ?? dog.Description;
        dog.Name = command.Name ?? dog.Name;

        return dog;
    }

    public static GetDogDto MapDogToGetDogDto(this Dog dog)
    {
        return new GetDogDto(
            dog.Id,
            dog.Name,
            dog.Age,
            dog.Weight,
            dog.Description);
    }

    public static PageOfTDataDto<GetDogDto> MapDogCollectionToPageODogDto(this IReadOnlyCollection<Dog> collection)
    {
        var collectionDto = new List<GetDogDto>();

        foreach (var dogOwner in collection)
        {
            collectionDto.Add(dogOwner.MapDogToGetDogDto());
        }

        return new PageOfTDataDto<GetDogDto>(collectionDto);
    }
}