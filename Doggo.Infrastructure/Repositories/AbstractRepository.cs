namespace Doggo.Infrastructure.Repositories;

using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class AbstractRepository<TEntity> : IAbstractRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DoggoDbContext _context;
    protected readonly DbSet<TEntity> Entities;

    public AbstractRepository(
        DoggoDbContext context)
    {
        _context = context;
        Entities = context.Set<TEntity>();
    }


    public async Task AddAsync(TEntity entity) => await Entities.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)));

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await Entities.AddRangeAsync(entities);

    public void Update(TEntity entity) => Entities.Update(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void UpdateRange(ICollection<TEntity> entities)
        => Entities.UpdateRange(entities ?? throw new ArgumentNullException(nameof(entities)));

    public void Remove(TEntity entity) => Entities.Remove(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void RemoveRange(ICollection<TEntity> collection)
        => Entities.RemoveRange(collection ?? throw new ArgumentNullException(nameof(collection)));

    public async Task SaveChanges() => await _context.SaveChangesAsync();
}