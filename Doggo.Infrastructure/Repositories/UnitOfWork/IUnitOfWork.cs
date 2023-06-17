namespace Doggo.Infrastructure.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository GetUserRepository();

    IDogOwnerRepository GetDogOwnerRepository();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}