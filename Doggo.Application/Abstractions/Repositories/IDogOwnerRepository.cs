namespace Doggo.Application.Abstractions.Repositories;

using Base;
using Domain.Entities.DogOwner;

public interface IDogOwnerRepository : IAbstractRepository<DogOwner>
{
    public Task<DogOwner?> GetAsync(Guid dogOwnerId, CancellationToken cancellationToken = default);

    public Task<DogOwner?> GetWithJobRequestAndJobsAsyncByUserId(Guid dogOwnerId, CancellationToken cancellationToken = default);
    public Task<DogOwner?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<DogOwner>> GetPageOfDogOwnersAsync(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}