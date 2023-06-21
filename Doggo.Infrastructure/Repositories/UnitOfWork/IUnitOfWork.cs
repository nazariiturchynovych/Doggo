namespace Doggo.Infrastructure.Repositories.UnitOfWork;

using Abstractions;

public interface IUnitOfWork
{
    IUserRepository GetUserRepository();

    IDogOwnerRepository GetDogOwnerRepository();

    IWalkerRepository GetWalkerRepository();

    IPossibleScheduleRepository GetPossibleScheduleRepository();

    IJobRepository GetJobRepository();

    IRequiredScheduleRepository GetRequiredScheduleRepository();

    IDogRepository GetDogRepository();

    IPersonalIdentifierRepository GetPersonalIdentifierRepository();

    IJobRequestRepository GetJobRequestRepository();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}