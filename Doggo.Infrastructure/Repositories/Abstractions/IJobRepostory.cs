namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.Job;

public interface IJobRepository : IAbstractRepository<Job>
{
    public Task<Job?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Job>> GetPageOfJobsAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);
}