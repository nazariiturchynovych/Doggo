namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.Chat;

public interface IChatRepository : IAbstractRepository<Chat>
{
    public Task<ICollection<Chat>?> GetAsync(Guid userId, CancellationToken cancellationToken = default);


    public Task<Chat?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}