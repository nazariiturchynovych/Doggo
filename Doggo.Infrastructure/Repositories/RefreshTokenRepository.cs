namespace Doggo.Infrastructure.Repositories;

using Application.Abstractions.Repositories;
using Base;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class RefreshTokenRepository: AbstractRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly DoggoDbContext _context;

    public RefreshTokenRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == id, cancellationToken: cancellationToken);
    }

    public async Task<RefreshToken?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken: cancellationToken);
    }
}