namespace Doggo.Infrastructure.Repositories;

using Application.Abstractions.Repositories;
using Base;
using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class UserChatRepository : AbstractRepository<UserChat>, IUserChatRepository
{
    private readonly DoggoDbContext _context;

    public UserChatRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<UserChat?> GetAsync(Guid chatId, Guid userId, CancellationToken cancellationToken = default)
    {
      return await _context.UserChats.FirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == userId, cancellationToken: cancellationToken);
    }
}