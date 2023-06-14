namespace Doggo.Infrastructure.Services.CacheService;

public interface ICacheService
{
    Task<T?> GetData<T>(string key);

    Task SetData<T>(string key, T value);

}