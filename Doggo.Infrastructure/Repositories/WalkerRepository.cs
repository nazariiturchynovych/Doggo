namespace Doggo.Infrastructure.Repositories;

using System.Linq.Expressions;
using Application.Abstractions.Repositories;
using Base;
using Domain.Constants;
using Domain.Entities.Walker;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class WalkerRepository : AbstractRepository<Walker>, IWalkerRepository
{
    private readonly DoggoDbContext _context;

    public WalkerRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Walker?> GetAsync(Guid walkerId, CancellationToken cancellationToken = default)
    {
        return await _context.Walkers.Where(x => x.Id == walkerId)
            .Include(x => x.User)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Walker?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Walkers
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Walker>> GetPageOfWalkersAsync(
        string? nameSearchTerm,
        string? skillSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Walker> walkerQuery = _context.Walkers
            .Include(x => x.User);
        if (!string.IsNullOrWhiteSpace(nameSearchTerm))
        {
            walkerQuery = walkerQuery.Where(
                x =>
                    x.User.FirstName.Contains(nameSearchTerm) || x.User.LastName.Contains(nameSearchTerm) );
        }

        if (!string.IsNullOrWhiteSpace(skillSearchTerm))
        {
            walkerQuery = walkerQuery.Where(
                x =>
                    x.Skills.Contains(skillSearchTerm) );
        }

        Expression<Func<Walker, object>> keySelector = sortColumn?.ToLower() switch
        {
            SortingConstants.About => walker => walker.About,
            SortingConstants.Skills => walker => walker.Skills,
            SortingConstants.FirstName => walker => walker.User.FirstName,
            SortingConstants.Lastname => walker => walker.User.LastName,
            SortingConstants.Age => walker => walker.User.Age,
            _ => walker => walker.User.Id,
        };

        walkerQuery = sortOrder?.ToLower() == SortingConstants.Descending
            ? walkerQuery.OrderByDescending(keySelector)
            : walkerQuery.OrderBy(keySelector);

        return await walkerQuery
            .Skip(pageCount * (page - 1))
            .Take(pageCount)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}