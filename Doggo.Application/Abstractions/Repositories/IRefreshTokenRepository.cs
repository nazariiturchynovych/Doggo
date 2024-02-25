namespace Doggo.Application.Abstractions.Repositories;

using Base;
using Domain.Entities.User;

public interface IRefreshTokenRepository: IAbstractRepository<RefreshToken>
{
    public Task<RefreshToken?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<RefreshToken?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default);
}