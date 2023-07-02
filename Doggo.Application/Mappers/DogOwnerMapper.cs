namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.DogOwner;
using Domain.Entities.DogOwner;
using Requests.Commands.DogOwner;

public static class DogOwnerMapper
{
    public static DogOwner MapDogOwnerUpdateCommandToDogOwner(this UpdateDogOwnerCommand updateDogOwnerCommand, DogOwner dogOwner)
    {
        dogOwner.Address = updateDogOwnerCommand.Address ?? dogOwner.Address;
        dogOwner.District = updateDogOwnerCommand.District ?? dogOwner.District;
        return dogOwner;
    }

    public static GetDogOwnerDto MapDogOwnerToGetDogOwnerDto(this DogOwner dogOwner)
    {
        return new GetDogOwnerDto(
            dogOwner.Id,
            dogOwner.UserId,
            dogOwner.Address,
            dogOwner.District,
            dogOwner.User.FirstName,
            dogOwner.User.LastName,
            dogOwner.User.PhoneNumber!,
            dogOwner.User.Email!);
    }


    public static PageOfTDataDto<GetDogOwnerDto> MapDogOwnerCollectionToPageODogOwnersDto(
        this IReadOnlyCollection<DogOwner> collection)
    {
        var collectionDto = collection.Select(dogOwner => dogOwner.MapDogOwnerToGetDogOwnerDto()).ToList();

        return new PageOfTDataDto<GetDogOwnerDto>(collectionDto);
    }
}