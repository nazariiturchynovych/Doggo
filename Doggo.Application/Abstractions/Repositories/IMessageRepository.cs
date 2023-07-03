namespace Doggo.Application.Abstractions.Persistence.Read;

using Domain.Entities.Chat;

public interface IMessageRepository : IAbstractRepository<Message>
{
    public Task<IReadOnlyCollection<Message>?> GetUserMessagesAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<Message?> GetAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Message>> GetChatMessagesAsync(
        Guid chatId,
        int? count,
        CancellationToken cancellationToken = default);
}