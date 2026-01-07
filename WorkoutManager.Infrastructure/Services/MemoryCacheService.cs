using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WorkoutManager.Application.Interfaces;

namespace WorkoutManager.Infrastructure.Services;

/// <summary>
/// Memóriában tárolt gyorsítótár szolgáltatás.
/// Lehetővé teszi értékek aszinkron lekérdezését vagy létrehozását, valamint törlését a gyorsítótárból.
/// </summary>
public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<MemoryCacheService> _logger;
    
    /// <summary>
    /// Konstruktor, amely a gyorsítótárat és a naplózót injektálja.
    /// </summary>
    public MemoryCacheService(IMemoryCache cache, ILogger<MemoryCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    
    /// <summary>
    /// Megpróbálja lekérni a megadott kulcshoz tartozó értéket a gyorsítótárból, vagy ha nem található, létrehozza azt a megadott függvénnyel.
    /// </summary>
    /// <typeparam name="T">A gyorsítótárban tárolt érték típusa.</typeparam>
    /// <param name="cacheKey">A gyorsítótár kulcsa.</param>
    /// <param name="factory">Az értéket előállító aszinkron függvény, ha nincs a gyorsítótárban.</param>
    /// <param name="absoluteExpireTime">Abszolút lejárati idő (opcionális).</param>
    /// <param name="slidingExpireTime">Csúszó lejárati idő (opcionális).</param>
    /// <returns>A gyorsítótárban lévő vagy újonnan létrehozott érték.</returns>
    public async Task<T?> GetOrCreateAsync<T>(
        string cacheKey,
        Func<Task<T>> factory,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? slidingExpireTime = null)
    {
        if (_cache.TryGetValue(cacheKey, out T? value))
        {
            _logger.LogInformation("Cache hit for key: {CacheKey}", cacheKey);
            return value;
        }
        
        value = await factory();
        
        var options = new MemoryCacheEntryOptions();
        if (absoluteExpireTime.HasValue)
            options.SetAbsoluteExpiration(absoluteExpireTime.Value);

        if (slidingExpireTime.HasValue)
            options.SetSlidingExpiration(slidingExpireTime.Value);

        _logger.LogInformation("Cache miss for key: {CacheKey}. Caching new value.", cacheKey);
        _cache.Set(cacheKey, value, options);

        return value;
    }
    
    /// <summary>
    /// Eltávolítja a megadott kulcshoz tartozó értéket a gyorsítótárból.
    /// </summary>
    /// <param name="cacheKey">A törlendő gyorsítótár kulcs.</param>
    public void Remove(string cacheKey)
    {
        _logger.LogInformation("Removing cache entry for key: {CacheKey}", cacheKey);
        _cache.Remove(cacheKey);
    }
}
