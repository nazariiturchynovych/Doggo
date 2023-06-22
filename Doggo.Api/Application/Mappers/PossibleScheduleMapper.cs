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