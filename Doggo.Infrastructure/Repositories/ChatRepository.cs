namespace Doggo.Infrastructure.Repositories;

using System.Linq.Expressions;
using Application.Abstractions.Persistence.Read;
using Base;
using Domain.Constants;
using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class ChatRepository : AbstractRepository<Chat>, IChatRepository
{
    private readonly DoggoDbContext _context;

    public ChatRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Chat>?> GetUserChatsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserChats.Include(x => x.Chat)
            .Where(x => x.UserId == userId)
            .Select(x => x.Chat)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Chat?> GetAsync(Guid chatId, CancellationToken cancellationToken = default)
    {
        return await _context.Chats.Include(x => x.UserChats).FirstOrDefaultAsync(x => x.Id == chatId, cancellationToken: cancellationToken);
    }

    public async Task<Chat?> GetWithMessages(Guid charId, CancellationToken cancellationToken = default)
    {
        return await _context.Chats
            .Include(x => x.Messages)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == charId, cancellationToken: cancellationToken);
    }
    
    public async Task<IReadOnlyCollection<Chat>> GetPageOfChatsAsync(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Chat> chatQuery = _context.Chats.Where(x => x.IsPrivate == false);
        if (!string.IsNullOrWhiteSpace(nameSearchTerm))
        {
            chatQuery = chatQuery.Where(
                x =>
                    x.Name.Contains(nameSearchTerm));
        }

        Expression<Func<Chat, object?>> keySelector = sortColumn?.ToLower() switch
        {
            SortingConstants.Name => chat => chat.Name,
            _ => chat => chat.Id,
        };

        chatQuery = sortOrder?.ToLower() == SortingConstants.Descending
            ? chatQuery.OrderByDescending(keySelector)
            : chatQuery.OrderBy(keySelector);

        return await chatQuery
            .Skip(pageCount * (page - 1))
            .Take(pageCount)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}