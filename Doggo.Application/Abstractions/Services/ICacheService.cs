namespace Doggo.Infrastructure.Services.CacheService;

public interface ICacheService
{
    Task<T?> GetData<T>(string key, CancellationToken cancellationToken = default);

    Task SetData<T>(string key, T value, CancellationToken cancellationToken =default);

    Task RemoveDataAsync(string key, CancellationToken cancellationToken = default);

    Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);

}