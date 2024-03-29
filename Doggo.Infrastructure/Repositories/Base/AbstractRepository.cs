namespace Doggo.Infrastructure.Repositories.Base;

using Application.Abstractions.Repositories.Base;
using Domain.Entities.Dog;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class AbstractRepository<TEntity> : IAbstractRepository<TEntity>
    where TEntity : class
{
    private readonly DoggoDbContext _context;
    private readonly DbSet<TEntity> _entities;

    // private readonly Exception _exception = new ArgumentNullException(nameof(TEntity)); // TODO make general exception to throw

    protected AbstractRepository(
        DoggoDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }


    public async Task AddAsync(TEntity entity)
        => await _entities.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)));

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _entities.AddRangeAsync(entities);

    public void Update(TEntity entity) => _entities.Update(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void Remove(TEntity entity) => _entities.Remove(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void RemoveRange(ICollection<TEntity> collection)
        => _entities.RemoveRange(collection ?? throw new ArgumentNullException(nameof(collection)));


    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}