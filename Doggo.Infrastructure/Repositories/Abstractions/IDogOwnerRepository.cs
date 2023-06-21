namespace Doggo.Infrastructure.Repositories.Abstractions;

using Doggo.Domain.Entities.DogOwner;

public interface IDogOwnerRepository : IAbstractRepository<DogOwner>
{
    public Task<DogOwner?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<DogOwner>> GetPageOfDogOwnersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int count,
        int page,
        CancellationToken cancellationToken = default);
}