namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.Job;

public interface IJobRepository : IAbstractRepository<Job>
{
    public Task<Job?> GetAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Job>> GetDogOwnerJobsAsync(
        Guid dogOwnerId,
        CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Job>> GetDogJobsAsync(
        Guid dogId,
        CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Job>> GetWalkerJobsAsync(
        Guid walkerId,
        CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Job>> GetPageOfJobsAsync(
        string? commentSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}