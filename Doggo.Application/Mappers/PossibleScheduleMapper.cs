namespace Doggo.Application.Mappers;

using Domain.Entities.Walker.Schedule;
using DTO;
using DTO.Walker.PossibleSchedule;

public static class PossibleScheduleMapper
{
    public static GetPossibleScheduleDto MapPossibleScheduleToGetPossibleScheduleDto(this PossibleSchedule possibleSchedule)
    {
        return new GetPossibleScheduleDto(
            possibleSchedule.Id,
            possibleSchedule.From,
            possibleSchedule.To);
    }

    public static PageOfTDataDto<GetPossibleScheduleDto> MapPossibleScheduleCollectionToPageOPossibleSchedulesDto(
        this IReadOnlyCollection<PossibleSchedule> collection)
    {
        var collectionDto = collection
            .Select(possibleSchedule => possibleSchedule.MapPossibleScheduleToGetPossibleScheduleDto())
            .ToList();

        return new PageOfTDataDto<GetPossibleScheduleDto>(collectionDto);
    }
}