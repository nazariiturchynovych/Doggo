namespace Doggo.Infrastructure.Repositories.UnitOfWork;

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

    public UnitOfWork(
        DoggoDbContext context,
        IUserRepository userRepository,
        IDogOwnerRepository dogOwnerRepository,
        IDogRepository dogRepository,
        IWalkerRepository walkerRepository,
        IPossibleScheduleRepository possibleScheduleRepository,
        IPersonalIdentifierRepository personalIdentifierRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _dogOwnerRepository = dogOwnerRepository;
        _dogRepository = dogRepository;
        _walkerRepository = walkerRepository;
        _possibleScheduleRepository = possibleScheduleRepository;
        _personalIdentifierRepository = personalIdentifierRepository;
    }


    public IUserRepository GetUserRepository() => _userRepository;

    public IDogOwnerRepository GetDogOwnerRepository() => _dogOwnerRepository;

    public IWalkerRepository GetWalkerRepository() => _walkerRepository;
    public IPossibleScheduleRepository GetPossibleScheduleRepository() => _possibleScheduleRepository;


    public IPersonalIdentifierRepository GetPersonalIdentifierRepository() => _personalIdentifierRepository;

    public IDogRepository GetDogRepository() => _dogRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
}