namespace Doggo.Application.Abstractions.Repositories.Base;

public interface IAbstractRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    void RemoveRange(ICollection<TEntity> collection);
}