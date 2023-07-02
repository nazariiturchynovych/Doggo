namespace Doggo.Infrastructure.Repositories.Abstractions;

using Doggo.Domain.Entities.DogOwner;

public interface IDogOwnerRepository : IAbstractRepository<DogOwner>
{
    public Task<DogOwner?> GetAsync(Guid dogOwnerId, CancellationToken cancellationToken = default);


    public Task<DogOwner?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<DogOwner>> GetPageOfDogOwnersAsync(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}