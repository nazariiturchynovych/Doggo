namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class MessageRepository : AbstractRepository<Message>, IMessageRepository
{
    private readonly DoggoDbContext _context;

    public MessageRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Message>?> GetUserMessagesAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Messages.Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Message?> GetAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken: cancellationToken);
    }


    public async Task<IReadOnlyCollection<Message>> GetChatMessagesAsync(
        Guid chatId,
        int? count,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Message> messageQuery = _context.Messages.OrderByDescending(x => x.CreatedDate);

        if (count is not null)
            messageQuery = messageQuery.Take(count.Value).Reverse();

        return await messageQuery
            .ToListAsync(cancellationToken: cancellationToken);
    }
}