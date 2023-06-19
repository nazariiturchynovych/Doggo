namespace Doggo.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IAbstractRepository<TEntity> where TEntity : class
{
    Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    public void UpdateRange(ICollection<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(ICollection<TEntity> collection);
}