namespace Doggo.Infrastructure.Repositories.UnitOfWork;

using Application.Abstractions.Repositories.UnitOfWork;
using Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DoggoDbContext _context;

    public UnitOfWork(
        DoggoDbContext context)
    {
        _context = context;
    }


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
}