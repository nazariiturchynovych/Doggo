namespace Doggo.Infrastructure.Services.CacheService;

using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> GetData<T>(string key)
    {
        var result = await _distributedCache.GetStringAsync(key);
        if (!result.IsNullOrEmpty())
            return JsonSerializer.Deserialize<T>(result!);
        return default!;
    }

    public async Task SetData<T>(
        string key,
        T value)
    {
        await _distributedCache.SetStringAsync(
            key,
            JsonSerializer.Serialize(value),
            new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10)

            });
    }

}