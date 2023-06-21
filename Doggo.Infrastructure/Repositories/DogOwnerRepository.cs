namespace Doggo.Infrastructure.Repositories;

using System.Linq.Expressions;
using Abstractions;
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

    public async Task<DogOwner?> GetAsync(Guid dogOwnerId, CancellationToken cancellationToken = default)
    {
        return await _context.DogOwners.Where(x => x.Id == dogOwnerId)
            .Include(x => x.User)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<DogOwner?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.DogOwners
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<DogOwner>> GetPageOfDogOwnersAsync(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<DogOwner> dogOwnerQuery = _context.DogOwners;
        if (!string.IsNullOrWhiteSpace(nameSearchTerm))
        {
            dogOwnerQuery = dogOwnerQuery.Where(
                x =>
                    x.User.FirstName.Contains(nameSearchTerm) || x.User.LastName.Contains(nameSearchTerm));
        }

        Expression<Func<DogOwner, object>> keySelector = sortColumn?.ToLower() switch
        {
            "district" => dogOwner => dogOwner.District,
            "address" => dogOwner => dogOwner.Address,
            "firstname" => dogOwner => dogOwner.User.FirstName,
            "lastname" => dogOwner => dogOwner.User.LastName,
            "age" => dogOwner => dogOwner.User.Age,
            _ => dogOwner => dogOwner.User.Id,
        };

        dogOwnerQuery = sortOrder?.ToLower() == "desc"
            ? dogOwnerQuery.OrderByDescending(keySelector)
            : dogOwnerQuery.OrderBy(keySelector);

        return await dogOwnerQuery
            .Skip(pageCount * (page - 1))
            .Take(pageCount)
            .Include(x => x.User)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}