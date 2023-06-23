namespace Doggo.Infrastructure.Repositories;

using System.Linq.Expressions;
using Abstractions;
using Domain.Constants;
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

    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Id == id)
            .Include(x => x.PersonalIdentifier)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<User>> GetPageOfUsersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<User> userQuery = _context.Users;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            userQuery = userQuery.Where(
                p =>
                    p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm));
        }

        Expression<Func<User, object>> keySelector = sortColumn?.ToLower() switch
        {
            SortingConstants.FirstName => user => user.FirstName,
            SortingConstants.Lastname => user => user.LastName,
            SortingConstants.Age => user => user.Age,
            _ => user => user.Id,
        };

        userQuery = sortOrder?.ToLower() == SortingConstants.Descending
            ? userQuery.OrderByDescending(keySelector)
            : userQuery.OrderBy(keySelector);

        return await userQuery
            .Skip(pageCount * (page - 1))
            .Take(pageCount)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<User?> GetUserWithRoles(string userEmail, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Email == userEmail)
            .Include(u => u.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<User?> GetUserWithRoles(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Id == userId)
            .Include(u => u.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}