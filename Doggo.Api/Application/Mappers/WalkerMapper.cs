namespace Doggo.Application.Mappers;

using Domain.DTO.Walker;
using Domain.Entities.Walker;

public static class WalkerMapper
{


    public static WalkerDto MapDogOwnerToDogOwnerDto(this Walker walker)
    {
        return new WalkerDto(walker.Id);
    }

}