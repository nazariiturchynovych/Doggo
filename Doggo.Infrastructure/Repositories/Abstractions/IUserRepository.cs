namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.User;

public interface IUserRepository
{
    public Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<User>> GetPageOfUsersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);

    public Task<User?> GetUserWithRoles(string userEmail, CancellationToken cancellationToken = default);

    public Task<User?> GetUserWithRoles(Guid userId, CancellationToken cancellationToken = default);
}