namespace Doggo.Application.Abstractions.Repositories;

using Domain.Entities.Walker;
using Persistence.Read;

public interface IWalkerRepository : IAbstractRepository<Walker>
{
    public Task<Walker?> GetAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<Walker?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Walker>> GetPageOfWalkersAsync(
        string? nameSearchTerm,
        string? skillSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}