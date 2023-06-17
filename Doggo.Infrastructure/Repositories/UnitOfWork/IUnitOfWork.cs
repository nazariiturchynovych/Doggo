namespace Doggo.Infrastructure.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository GetUserRepository();

    IDogOwnerRepository GetDogOwnerRepository();

    IDogRepository GetDogRepository();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}