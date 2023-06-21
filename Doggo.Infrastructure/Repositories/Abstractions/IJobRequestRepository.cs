namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.JobRequest;

public interface IJobRequestRepository : IAbstractRepository<JobRequest>
{
    public Task<JobRequest?> GetAsync(Guid userId, CancellationToken cancellationToken = default);


    public Task<IReadOnlyCollection<JobRequest>> GetDogOwnerJobRequests(
        Guid dogOwnerId,
        CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<JobRequest>> GetPageOfJobRequestsAsync(
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}