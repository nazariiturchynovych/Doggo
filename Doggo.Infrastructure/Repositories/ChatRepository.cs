namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.Chat;
using Domain.Entities.Walker.Schedule;
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

    public async Task<ICollection<Chat>?> GetAsync(Guid possibleScheduleId, CancellationToken cancellationToken = default)
    {
        return await _context.Chats.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Chat?> GetByIdAsync(Guid charId, CancellationToken cancellationToken = default)
    {
        return await _context.Chats.FirstOrDefaultAsync(x => x.Id == charId, cancellationToken: cancellationToken);
    }
}