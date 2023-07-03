namespace Doggo.Infrastructure.Repositories.UnitOfWork;

using Application.Abstractions.Persistence.Read;

public interface IUnitOfWork
{
    IUserRepository GetUserRepository();

    IDogOwnerRepository GetDogOwnerRepository();

    IWalkerRepository GetWalkerRepository();

    IChatRepository GetChatRepository();

    IUserChatRepository GetUserChatRepository();

    IMessageRepository GetMessageRepository();

    IPossibleScheduleRepository GetPossibleScheduleRepository();

    IJobRepository GetJobRepository();

    IRequiredScheduleRepository GetRequiredScheduleRepository();

    IDogRepository GetDogRepository();

    IPersonalIdentifierRepository GetPersonalIdentifierRepository();

    IJobRequestRepository GetJobRequestRepository();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}