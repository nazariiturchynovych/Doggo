namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.Dog;
using Domain.Entities.DogOwner;

public interface IDogRepository : IAbstractRepository<Dog>
{
    public Task<Dog?> GetAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Dog>> GetDogOwnerDogsAsync(Guid dogOwnerId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Dog>> GetPageOfDogsAsync(
        string? nameSearchTerm,
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}