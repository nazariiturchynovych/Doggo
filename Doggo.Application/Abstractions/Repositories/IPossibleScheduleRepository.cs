namespace Doggo.Application.Abstractions.Persistence.Read;

using Domain.Entities.Walker.Schedule;

public interface IPossibleScheduleRepository : IAbstractRepository<PossibleSchedule>
{
    public Task<PossibleSchedule?> GetAsync(Guid possibleScheduleId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<PossibleSchedule>> GetWalkerPossibleSchedulesAsync(
        Guid walkerId,
        CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<PossibleSchedule>> GetPageOfPossibleSchedulesAsync(
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}