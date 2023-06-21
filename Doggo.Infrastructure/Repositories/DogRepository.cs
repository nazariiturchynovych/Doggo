namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.DogOwner;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class DogRepository : AbstractRepository<Dog>, IDogRepository
{
    private readonly DoggoDbContext _context;

    public DogRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Dog?> GetAsync(int dogId, CancellationToken cancellationToken = default)
    {
        return await _context.Dogs.FirstOrDefaultAsync(x => x.Id == dogId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Dog>> GetDogOwnerDogsAsync(
        int dogOwnerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Dogs.Where(x => x.DogOwnerId == dogOwnerId)
            .OrderBy(ps => ps.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Dog>> GetPageOfDogsAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default)
    {
        return await _context.Dogs.OrderBy(ps => ps.Id)
            .Skip(count * (page - 1))
            .Take(count)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}