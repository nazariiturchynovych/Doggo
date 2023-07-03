namespace Doggo.Application.Abstractions.Persistence.Read;

using Domain.Entities.Chat;

public interface IChatRepository : IAbstractRepository<Chat>
{
    public Task<IReadOnlyCollection<Chat>?> GetUserChatsAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<Chat?> GetWithMessages(Guid chatId, CancellationToken cancellationToken = default);

    public Task<Chat?> GetAsync(Guid chatId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyCollection<Chat>> GetPageOfChatsAsync(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default);
}