namespace Doggo.Application.Mappers;

using Domain.Entities.Walker;
using DTO;
using DTO.Walker;
using Requests.Commands.Walker;

public static class WalkerMapper
{
    public static Walker MapWalkerUpdateCommandToWalker(this UpdateWalkerCommand updateWalkerCommand, Walker walker)
    {
        walker.Skills = updateWalkerCommand.Skills ?? walker.Skills;
        walker.About = updateWalkerCommand.About ?? walker.About;
        return walker;
    }

    public static GetWalkerDto MapWalkerToGetWalkerDto(this Walker walker)
    {
        return new GetWalkerDto(
            walker.Id,
            walker.UserId,
            walker.Skills,
            walker.About,
            walker.User.FirstName,
            walker.User.LastName,
            walker.User.PhoneNumber!,
            walker.User.Email!);
    }


    public static PageOfTDataDto<GetWalkerDto> MapWalkerCollectionToPageOWalkersDto(this IReadOnlyCollection<Walker> collection)
    {
        var collectionDto = collection.Select(walker => walker.MapWalkerToGetWalkerDto()).ToList();

        return new PageOfTDataDto<GetWalkerDto>(collectionDto);
    }
}