namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.Walker;

public interface IWalkerRepository : IAbstractRepository<Walker>
{
    public Task<Walker?> GetAsync(int userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Walker>> GetPageOfWalkersAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);
}