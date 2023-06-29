namespace Doggo.Infrastructure.Services.CacheService;

using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    private readonly ConcurrentDictionary<string, bool> _cacheKeys = new();

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> GetData<T>(string key, CancellationToken cancellationToken = default)
    {
        var result = await _distributedCache.GetStringAsync(key, token: cancellationToken);
        if (!result.IsNullOrEmpty())
            return JsonSerializer.Deserialize<T>(result!);
        return default!;
    }

    public async Task SetData<T>(
        string key,
        T value,
        CancellationToken cancellationToken = default)
    {
        await _distributedCache.SetStringAsync(
            key,
            JsonSerializer.Serialize(value),
            new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            },
            token: cancellationToken);

        _cacheKeys.TryAdd(key, true);
    }

    public async Task RemoveDataAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);

        _cacheKeys.TryRemove(key, out _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = _cacheKeys.Keys.Where(x => x.StartsWith(prefixKey))
            .Select(xx => RemoveDataAsync(xx, cancellationToken));

        await Task.WhenAll(tasks);
    }
}