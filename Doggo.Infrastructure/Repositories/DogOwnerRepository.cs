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

    public async Task<DogOwner?> GetAsync(int dogOwnerId, CancellationToken cancellationToken = default)
    {
        return await _context.DogOwners
            .Include(x => x.Dogs)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == dogOwnerId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<DogOwner>> GetPageOfDogOwnersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int count,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<DogOwner> dogOwnerQuery = _context.DogOwners;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            dogOwnerQuery = dogOwnerQuery.Where(
                x =>
                    x.User.FirstName.Contains(searchTerm) || x.User.LastName.Contains(searchTerm));
        }

        Expression<Func<DogOwner, object>> keySelector = sortColumn?.ToLower() switch
        {
            "district" => dogOwner => dogOwner.District,
            "address" => dogOwner => dogOwner.Address,
            "firstName" => dogOwner => dogOwner.User.FirstName,
            "lastName" => dogOwner => dogOwner.User.LastName,
            "age" => dogOwner => dogOwner.User.Age,
            _ => dogOwner => dogOwner.User.Id,
        };

        dogOwnerQuery = sortOrder?.ToLower() == "desc"
            ? dogOwnerQuery.OrderByDescending(keySelector)
            : dogOwnerQuery.OrderBy(keySelector);

        return await dogOwnerQuery
            .Skip(count * (page - 1))
            .Take(count)
            .Include(x => x.User)
            .Include(x => x.Dogs)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}