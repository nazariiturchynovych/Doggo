namespace Doggo.Infrastructure.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository GetUserRepository();

    IDogOwnerRepository GetDogOwnerRepository();

    IWalkerRepository GetWalkerRepository();

    IPossibleScheduleRepository GetPossibleScheduleRepository();

    IDogRepository GetDogRepository();

    IPersonalIdentifierRepository GetPersonalIdentifierRepository();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}