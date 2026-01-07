namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Általános CRUD (Create, Read, Update, Delete) szolgáltatás interfész.
/// Lehetővé teszi entitások létrehozását, módosítását, törlését, lekérdezését és listázását.
/// </summary>
/// <typeparam name="TEntity">Az entitás típusa.</typeparam>
public interface ICrudService<TEntity> where TEntity : class
{
    /// <summary>
    /// Új entitás létrehozása.
    /// </summary>
    /// <param name="entity">A létrehozandó entitás.</param>
    /// <returns>A létrehozott entitás.</returns>
    Task<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    /// Entitás módosítása.
    /// </summary>
    /// <param name="id">A módosítandó entitás azonosítója.</param>
    /// <param name="entity">A módosított entitás adatai.</param>
    /// <returns>A módosított entitás.</returns>
    Task<TEntity> UpdateAsync(int id, TEntity entity);

    /// <summary>
    /// Entitás törlése.
    /// </summary>
    /// <param name="id">A törlendő entitás azonosítója.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Minden entitás lekérdezése, opcionálisan a töröltekkel együtt.
    /// </summary>
    /// <param name="includeDeleted">Tartalmazza-e a törölt entitásokat.</param>
    /// <returns>Az entitások listája.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false);

    /// <summary>
    /// Egy entitás lekérdezése azonosító alapján, opcionálisan a töröltekkel együtt.
    /// </summary>
    /// <param name="id">Az entitás azonosítója.</param>
    /// <param name="includeDeleted">Tartalmazza-e a törölt entitásokat.</param>
    /// <returns>Az entitás vagy null, ha nem található.</returns>
    Task<TEntity?> GetByIdAsync(int id, bool includeDeleted = false);
}