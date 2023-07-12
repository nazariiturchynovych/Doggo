namespace Doggo.Application.Mappers;

using Domain.Entities.Walker.Schedule;
using Responses;
using Responses.Walker.PossibleSchedule;

public static class PossibleScheduleMapper
{
    public static PossibleScheduleResponse MapPossibleScheduleToPossibleScheduleResponse(this PossibleSchedule possibleSchedule)
    {
        return new PossibleScheduleResponse(
            possibleSchedule.Id,
            possibleSchedule.From,
            possibleSchedule.To);
    }

    public static PageOf<PossibleScheduleResponse> MapPossibleScheduleCollectionToPageOPossibleSchedulesResponse(
        this IReadOnlyCollection<PossibleSchedule> collection)
    {
        var collectionDto = collection
            .Select(possibleSchedule => possibleSchedule.MapPossibleScheduleToPossibleScheduleResponse())
            .ToList();

        return new PageOf<PossibleScheduleResponse>(collectionDto);
    }

    public static List<PossibleScheduleResponse> MapPossibleScheduleCollectionToListOPossibleSchedulesResponse(
        this IReadOnlyCollection<PossibleSchedule> collection)
    {
        return collection
            .Select(possibleSchedule => possibleSchedule.MapPossibleScheduleToPossibleScheduleResponse())
            .ToList();

    }
}