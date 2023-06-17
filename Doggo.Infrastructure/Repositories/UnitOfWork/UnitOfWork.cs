namespace Doggo.Infrastructure.Repositories.UnitOfWork;

using Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DoggoDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public UnitOfWork(
        DoggoDbContext context,
        IUserRepository userRepository,
        IDogOwnerRepository dogOwnerRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _dogOwnerRepository = dogOwnerRepository;
    }


    public IUserRepository GetUserRepository() => _userRepository;

    public IDogOwnerRepository GetDogOwnerRepository() => _dogOwnerRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
}