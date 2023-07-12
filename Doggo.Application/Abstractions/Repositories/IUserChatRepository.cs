namespace Doggo.Application.Abstractions.Repositories;

using Base;
using Domain.Entities.Chat;

public interface IUserChatRepository : IAbstractRepository<UserChat>
{
    public Task<UserChat?> GetAsync(Guid chatId,Guid userId, CancellationToken cancellationToken = default);
}