namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.JobRequest.Schedules;

public interface IRequiredScheduleRepository : IAbstractRepository<RequiredSchedule>
{
    public Task<RequiredSchedule?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<RequiredSchedule>> GetPageOfRequiredSchedulesAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);
}