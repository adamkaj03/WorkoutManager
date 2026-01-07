namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Általános gyorsítótár szolgáltatás interfész.
/// Lehetővé teszi értékek lekérdezését vagy létrehozását, valamint törlését a gyorsítótárból.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Megpróbálja lekérni a megadott kulcshoz tartozó értéket a gyorsítótárból, vagy ha nem található, létrehozza azt a megadott függvénnyel.
    /// </summary>
    /// <typeparam name="T">A gyorsítótárban tárolt érték típusa.</typeparam>
    /// <param name="cacheKey">A gyorsítótár kulcsa.</param>
    /// <param name="factory">Az értéket előállító aszinkron függvény, ha nincs a gyorsítótárban.</param>
    /// <param name="absoluteExpireTime">Abszolút lejárati idő (opcionális).</param>
    /// <param name="slidingExpireTime">Csúszó lejárati idő (opcionális).</param>
    /// <returns>A gyorsítótárban lévő vagy újonnan létrehozott érték.</returns>
    Task<T?> GetOrCreateAsync<T>(
        string cacheKey,
        Func<Task<T>> factory,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? slidingExpireTime = null);
    
    /// <summary>
    /// Eltávolítja a megadott kulcshoz tartozó értéket a gyorsítótárból.
    /// </summary>
    /// <param name="cacheKey">A törlendő gyorsítótár kulcs.</param>
    void Remove(string cacheKey);
}
