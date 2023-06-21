namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.JobRequest;

public interface IJobRequestRepository : IAbstractRepository<JobRequest>
{
    public Task<JobRequest?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<JobRequest>> GetPageOfJobRequestsAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);
}