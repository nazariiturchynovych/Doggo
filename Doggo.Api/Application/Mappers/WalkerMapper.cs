namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.Walker;
using Domain.DTO.Walker.PossibleSchedule;
using Domain.Entities.Walker;
using Requests.Commands.Walker;

public static class WalkerMapper
{
    public static WalkerDto MapWalkerToWalkerDto(this Walker walker)
    {
        return new WalkerDto(walker.Id);
    }

    public static Walker MapWalkerUpdateCommandToWalker(this UpdateWalkerCommand updateWalkerCommand, Walker walker)
    {
        walker.Skills = updateWalkerCommand.Skills ?? walker.Skills;
        walker.About = updateWalkerCommand.About ?? walker.About;
        return walker;
    }

    public static GetWalkerDto MapWalkerToGetWalkerDto(this Walker walker)
    {
        var possibleSchedules = new List<GetPossibleScheduleDto>();

        foreach (var possibleSchedule in walker.PossibleSchedules)
        {
            possibleSchedules.Add(possibleSchedule.MapPossibleScheduleToGetPossibleScheduleDto());
        }

        return new GetWalkerDto(
            walker.Id,
            walker.Skills,
            walker.About,
            walker.User.FirstName,
            walker.User.LastName,
            walker.User.PhoneNumber!,
            walker.User.Email!,
            possibleSchedules);
    }


    public static PageOfTDataDto<GetWalkerDto> MapWalkerCollectionToPageOWalkersDto(this IReadOnlyCollection<Walker> collection)
    {
        var collectionDto = new List<GetWalkerDto>();

        foreach (var walker in collection)
        {
            collectionDto.Add(walker.MapWalkerToGetWalkerDto());
        }

        return new PageOfTDataDto<GetWalkerDto>(collectionDto);
    }
}