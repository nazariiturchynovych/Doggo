namespace Doggo.Infrastructure.Repositories.UnitOfWork;

using Application.Abstractions.Persistence.Read;
using Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DoggoDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;
    private readonly IDogRepository _dogRepository;
    private readonly IWalkerRepository _walkerRepository;
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;
    private readonly IPersonalIdentifierRepository _personalIdentifierRepository;
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly IRequiredScheduleRepository _requiredScheduleRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IUserChatRepository _userChatRepository;
    private readonly IMessageRepository _messageRepository;

    public UnitOfWork(
        DoggoDbContext context,
        IUserRepository userRepository,
        IDogOwnerRepository dogOwnerRepository,
        IDogRepository dogRepository,
        IWalkerRepository walkerRepository,
        IPossibleScheduleRepository possibleScheduleRepository,
        IPersonalIdentifierRepository personalIdentifierRepository,
        IJobRequestRepository jobRequestRepository,
        IRequiredScheduleRepository requiredScheduleRepository,
        IJobRepository jobRepository,
        IChatRepository chatRepository,
        IUserChatRepository userChatRepository,
        IMessageRepository messageRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _dogOwnerRepository = dogOwnerRepository;
        _dogRepository = dogRepository;
        _walkerRepository = walkerRepository;
        _possibleScheduleRepository = possibleScheduleRepository;
        _personalIdentifierRepository = personalIdentifierRepository;
        _jobRequestRepository = jobRequestRepository;
        _requiredScheduleRepository = requiredScheduleRepository;
        _jobRepository = jobRepository;
        _chatRepository = chatRepository;
        _userChatRepository = userChatRepository;
        _messageRepository = messageRepository;
    }


    public IUserRepository GetUserRepository() => _userRepository;

    public IDogOwnerRepository GetDogOwnerRepository() => _dogOwnerRepository;

    public IWalkerRepository GetWalkerRepository() => _walkerRepository;

    public IChatRepository GetChatRepository() => _chatRepository;

    public IUserChatRepository GetUserChatRepository() => _userChatRepository;

    public IMessageRepository GetMessageRepository() => _messageRepository;

    public IPossibleScheduleRepository GetPossibleScheduleRepository() => _possibleScheduleRepository;

    public IJobRepository GetJobRepository() => _jobRepository;

    public IRequiredScheduleRepository GetRequiredScheduleRepository() => _requiredScheduleRepository;

    public IPersonalIdentifierRepository GetPersonalIdentifierRepository() => _personalIdentifierRepository;

    public IJobRequestRepository GetJobRequestRepository() => _jobRequestRepository;

    public IDogRepository GetDogRepository() => _dogRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
}