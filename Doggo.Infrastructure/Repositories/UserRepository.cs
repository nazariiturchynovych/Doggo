namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class UserRepository : IUserRepository
{
    private readonly DoggoDbContext _context;

    public UserRepository(DoggoDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Id == id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<User>> GetPageOfUsersAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users.OrderBy(ps => ps.Id)
            .Skip(count * (page - 1))
            .Take(count)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<User?> GetUserWithRoles(string userEmail, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Email == userEmail)
            .Include(u => u.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<User?> GetUserWithRoles(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Id == userId)
            .Include(u => u.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}