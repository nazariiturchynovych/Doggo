namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.Walker.PossibleSchedule;
using Domain.Entities.Walker.Schedule;

public static class PossibleScheduleMapper
{
    public static GetPossibleScheduleDto MapPossibleScheduleToGetPossibleScheduleDto(this PossibleSchedule possibleSchedule)
    {
        return new GetPossibleScheduleDto(
            possibleSchedule.Id,
            possibleSchedule.From,
            possibleSchedule.To,
            possibleSchedule.DayOfWeek);
    }
    
    public static PageOfTDataDto<GetPossibleScheduleDto> MapPossibleScheduleCollectionToPageOPossibleSchedulesDto(this IReadOnlyCollection<PossibleSchedule> collection)
    {
        var collectionDto = new List<GetPossibleScheduleDto>();

        foreach (var possibleSchedule in collection)
        {
            collectionDto.Add(possibleSchedule.MapPossibleScheduleToGetPossibleScheduleDto());
        }
        return new PageOfTDataDto<GetPossibleScheduleDto>(collectionDto);
    }
}