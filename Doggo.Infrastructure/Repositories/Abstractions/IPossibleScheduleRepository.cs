namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.Walker.Schedule;

public interface IPossibleScheduleRepository : IAbstractRepository<PossibleSchedule>
{
    public Task<PossibleSchedule?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<PossibleSchedule>> GetPageOfPossibleSchedulesAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);
}