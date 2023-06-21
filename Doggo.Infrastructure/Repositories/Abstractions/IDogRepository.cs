namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.DogOwner;

public interface IDogRepository : IAbstractRepository<Dog>
{
    public Task<Dog?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Dog>> GetPageOfDogsAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);
}