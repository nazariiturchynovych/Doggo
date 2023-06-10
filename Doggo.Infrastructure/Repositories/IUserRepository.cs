namespace Doggo.Infrastructure.Repositories;

using Doggo.Domain.Entities.User;

public interface IUserRepository
{
    public Task<User> GetAsync(int id, CancellationToken cancellationToken = default);

    public Task<User> GetUserWithRoles(int id, CancellationToken cancellationToken = default);
}