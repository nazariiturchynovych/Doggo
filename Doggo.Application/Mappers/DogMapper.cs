namespace Doggo.Application.Mappers;

using Domain.Entities.Dog;
using DTO;
using DTO.Dog;
using Requests.Commands.Dog;
using Requests.Commands.Dog.UpdateDogCommand;

public static class DogMapper
{
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
            dog.DogOwnerId,
            dog.Name,
            dog.Age,
            dog.Weight,
            dog.Description);
    }

    public static PageOfTDataDto<GetDogDto> MapDogCollectionToPageOfDogDto(this IReadOnlyCollection<Dog> collection)
    {
        var collectionDto = collection.Select(dogOwner => dogOwner.MapDogToGetDogDto()).ToList();

        return new PageOfTDataDto<GetDogDto>(collectionDto);
    }
}