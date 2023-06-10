namespace Doggo.Infrastructure.UnitOfWork;

using Domain.Repositories;
using Domain.Repositories.UnitOfWork;
using Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly DoggoDbContext _context;
    private readonly IUserRepository _userRepository;

    public UnitOfWork(
        DoggoDbContext context,
        IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }


    public IUserRepository GetUserRepository() => _userRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
}