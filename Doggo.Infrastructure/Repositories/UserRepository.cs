namespace Doggo.Infrastructure.Repositories;

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


    public async Task<User?> GetUserWithRoles(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Id == id)
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}