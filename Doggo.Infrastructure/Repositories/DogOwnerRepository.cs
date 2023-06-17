namespace Doggo.Infrastructure.Repositories;

using Domain.Entities.DogOwner;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class DogOwnerRepository : AbstractRepository<DogOwner>, IDogOwnerRepository
{
    private readonly DoggoDbContext _context;

    public DogOwnerRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<DogOwner?> GetAsync(int dogOwnerId, CancellationToken cancellationToken = default)
    {
        return await _context.DogOwners.FirstOrDefaultAsync(x => x.Id == dogOwnerId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<DogOwner>> GetPageOfDogOwnersAsync(int count, int page, CancellationToken cancellationToken = default)
    {
        return await _context.DogOwners.OrderBy(ps => ps.Id)
                    .Skip(count * (page - 1))
                    .Take(count)
                    .ToListAsync(cancellationToken: cancellationToken);
    }
}