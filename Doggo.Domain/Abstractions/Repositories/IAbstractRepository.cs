namespace Doggo.Infrastructure.Repositories.Abstractions;

using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IAbstractRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    public void UpdateRange(ICollection<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(ICollection<TEntity> collection);
}