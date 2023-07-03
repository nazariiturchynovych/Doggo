namespace Doggo.Application.Abstractions.Repositories.UnitOfWork;

public interface IUnitOfWork
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}