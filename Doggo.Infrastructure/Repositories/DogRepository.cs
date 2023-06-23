namespace Doggo.Infrastructure.Repositories;

using System.Linq.Expressions;
using Abstractions;
using Domain.Constants;
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

    public async Task<Dog?> GetAsync(Guid dogId, CancellationToken cancellationToken = default)
    {
        return await _context.Dogs.FirstOrDefaultAsync(x => x.Id == dogId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Dog>> GetDogOwnerDogsAsync(
        Guid dogOwnerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Dogs.Where(x => x.DogOwnerId == dogOwnerId)
            .OrderBy(ps => ps.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Dog>> GetPageOfDogsAsync(
        string? nameSearchTerm,
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Dog> dogQuery = _context.Dogs;
        if (!string.IsNullOrWhiteSpace(nameSearchTerm))
        {
            dogQuery = dogQuery.Where(
                x =>
                    x.Name.Contains(nameSearchTerm));
        }

        if (!string.IsNullOrWhiteSpace(descriptionSearchTerm))
        {
            dogQuery = dogQuery.Where(
                x =>
                    x.Description.Contains(descriptionSearchTerm));
        }

        Expression<Func<Dog, object?>> keySelector = sortColumn?.ToLower() switch
        {
            SortingConstants.Name => dog => dog.Name,
            SortingConstants.Description => dog => dog.Description,
            SortingConstants.Age => dog => dog.Age,
            SortingConstants.Weight => dog => dog.Weight,
            _ => dog => dog.Id,
        };

        dogQuery = sortOrder?.ToLower() == SortingConstants.Descending
            ? dogQuery.OrderByDescending(keySelector)
            : dogQuery.OrderBy(keySelector);

        return await dogQuery
            .Skip(pageCount * (page - 1))
            .Take(pageCount)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}