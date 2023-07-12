namespace Doggo.Application.Mappers;

using Domain.Entities.Dog;
using Requests.Commands.Dog.UpdateDogCommand;
using Responses;
using Responses.Dog;

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

    public static DogResponse MapDogToDogResponse(this Dog dog)
    {
        return new DogResponse(
            dog.Id,
            dog.DogOwnerId,
            dog.Name,
            dog.Age,
            dog.Weight,
            dog.Description);
    }

    public static PageOf<DogResponse> MapDogCollectionToPageOfDogResponse(this IReadOnlyCollection<Dog> collection)
    {
        var collectionDto = collection.Select(dogOwner => dogOwner.MapDogToDogResponse()).ToList();

        return new PageOf<DogResponse>(collectionDto);
    }

    public static List<DogResponse> MapDogCollectionToListOfDogResponse(this IReadOnlyCollection<Dog> collection)
    {
        return collection.Select(dogOwner => dogOwner.MapDogToDogResponse()).ToList();
    }
}