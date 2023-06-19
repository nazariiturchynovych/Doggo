namespace Doggo.Infrastructure.Repositories;

using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence;

public class AbstractRepository<TEntity> : IAbstractRepository<TEntity>
    where TEntity : class, IEntity
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


    public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity) => await _entities.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)));

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _entities.AddRangeAsync(entities);

    public void Update(TEntity entity) => _entities.Update(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void UpdateRange(ICollection<TEntity> entities)
        => _entities.UpdateRange(entities ?? throw new ArgumentNullException(nameof(entities)));

    public void Remove(TEntity entity) => _entities.Remove(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void RemoveRange(ICollection<TEntity> collection)
        => _entities.RemoveRange(collection ?? throw new ArgumentNullException(nameof(collection)));

    public async Task SaveChanges() => await _context.SaveChangesAsync();
}