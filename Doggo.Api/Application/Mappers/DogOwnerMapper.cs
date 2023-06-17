namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.Dog;
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
        var dogs = new List<DogDto>();

        foreach (var dog in dogOwner.Dogs)
        {
            dogs.Add(dog.MapDogToDogDto());
        }

        return new GetDogOwnerDto(dogOwner.Id, dogOwner.Address, dogOwner.District, dogs);
    }

    public static DogOwnerDto MapDogOwnerToDogOwnerDto(this DogOwner dogOwner)
    {
        return new DogOwnerDto(dogOwner.Id);
    }


    public static PageOfTDataDto<GetDogOwnerDto> MapUserCollectionToPageODogOwnersDto(this IReadOnlyCollection<DogOwner> collection)
    {
        var collectionDto = new List<GetDogOwnerDto>();

        foreach (var dogOwner in collection)
        {
            collectionDto.Add(dogOwner.MapDogOwnerToGetDogOwnerDto());
        }
        return new PageOfTDataDto<GetDogOwnerDto>(collectionDto);
    }
}