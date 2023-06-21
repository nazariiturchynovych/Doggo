namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.User;

public interface IUserRepository
{
    public Task<User?> GetAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<User>> GetPageOfUsersAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default);

    public Task<User?> GetUserWithRoles(string userEmail, CancellationToken cancellationToken = default);

    public Task<User?> GetUserWithRoles(int userId, CancellationToken cancellationToken = default);
}