namespace Doggo.Application.Abstractions.Persistence.Read;

using Domain.Entities.JobRequest.Schedule;

public interface IRequiredScheduleRepository : IAbstractRepository<RequiredSchedule>
{
    public Task<RequiredSchedule?> GetAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<RequiredSchedule>> GetPageOfRequiredSchedulesAsync(
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}