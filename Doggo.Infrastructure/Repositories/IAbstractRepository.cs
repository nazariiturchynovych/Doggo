namespace Doggo.Infrastructure.Repositories;

public interface IAbstractRepository<TEntity>
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    public void UpdateRange(ICollection<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(ICollection<TEntity> collection);
}