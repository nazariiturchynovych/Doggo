namespace Doggo.Infrastructure.Repositories;

using Abstractions;
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

    public async Task<Walker?> GetAsync(int dogOwnerId, CancellationToken cancellationToken = default)
    {
        return await _context.Walkers.FirstOrDefaultAsync(x => x.Id == dogOwnerId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Walker>> GetPageOfWalkersAsync(int count, int page, CancellationToken cancellationToken = default)
    {
        return await _context.Walkers.OrderBy(ps => ps.Id)
                    .Skip(count * (page - 1))
                    .Take(count)
                    .ToListAsync(cancellationToken: cancellationToken);
    }
}