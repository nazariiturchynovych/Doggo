namespace Doggo.Application.Mappers;

using Domain.Entities.DogOwner;
using Requests.Commands.DogOwner.UpdateDogOwnerCommand;
using Responses;
using Responses.DogOwner;

public static class DogOwnerMapper
{
    public static DogOwner MapDogOwnerUpdateCommandToDogOwner(this UpdateDogOwnerCommand updateDogOwnerCommand, DogOwner dogOwner)
    {
        dogOwner.Address = updateDogOwnerCommand.Address ?? dogOwner.Address;
        dogOwner.District = updateDogOwnerCommand.District ?? dogOwner.District;
        return dogOwner;
    }

    public static DogOwnerResponse MapDogOwnerToDogOwnerResponse(this DogOwner dogOwner)
    {
        return new DogOwnerResponse(
            dogOwner.Id,
            dogOwner.UserId,
            dogOwner.Address,
            dogOwner.District,
            dogOwner.User.FirstName,
            dogOwner.User.LastName,
            dogOwner.User.PhoneNumber!,
            dogOwner.User.Email!);
    }


    public static PageOf<DogOwnerResponse> MapDogOwnerCollectionToPageOfDogOwnersResponse(
        this IReadOnlyCollection<DogOwner> collection)
    {
        var collectionDto = collection.Select(dogOwner => dogOwner.MapDogOwnerToDogOwnerResponse()).ToList();

        return new PageOf<DogOwnerResponse>(collectionDto);
    }
}