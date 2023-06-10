namespace Doggo.Infrastructure.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository GetUserRepository();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}