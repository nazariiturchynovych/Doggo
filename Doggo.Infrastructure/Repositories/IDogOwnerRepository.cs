namespace Doggo.Infrastructure.Repositories;

using Domain.Entities.DogOwner;

public interface IDogOwnerRepository : IAbstractRepository<DogOwner>
{
    public Task<DogOwner?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<DogOwner>> GetPageOfDogOwnersAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);
}