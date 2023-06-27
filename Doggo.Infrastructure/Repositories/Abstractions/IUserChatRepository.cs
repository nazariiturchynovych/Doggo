namespace Doggo.Infrastructure.Repositories.Abstractions;

using Domain.Entities.Chat;

public interface IUserChatRepository : IAbstractRepository<UserChat>
{
    public Task<UserChat?> GetAsync(Guid chatId,Guid userId, CancellationToken cancellationToken = default);
}