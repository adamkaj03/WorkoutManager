using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;
using WorkoutManager.Shared.Exceptions;

namespace WorkoutManager.Infrastructure.Services;

/// <summary>
/// Általános CRUD (Create, Read, Update, Delete) szolgáltatás implementáció.
/// Lehetővé teszi entitások létrehozását, módosítását, törlését, lekérdezését és listázását.
/// </summary>
/// <typeparam name="TEntity">Az entitás típusa.</typeparam>
public class CrudService<TEntity>(IRepository<TEntity> repository, IUnitOfWork unitOfWork) : ICrudService<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Új entitás létrehozása és mentése az adatbázisba.
    /// </summary>
    /// <param name="entity">A létrehozandó entitás.</param>
    /// <returns>A létrehozott entitás.</returns>
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Entitás módosítása azonosító alapján.
    /// </summary>
    /// <param name="id">A módosítandó entitás azonosítója.</param>
    /// <param name="entity">A módosított entitás adatai.</param>
    /// <returns>A módosított entitás.</returns>
    public async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        var existing = await repository.FirstOrDefaultAsync(e => e.Id == id);
        if (existing == null) throw new NotFoundException("Entity not found");
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Entitás törlése azonosító alapján.
    /// </summary>
    /// <param name="id">A törlendő entitás azonosítója.</param>
    public async Task DeleteAsync(int id)
    {
        var entity = await repository.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null) throw new NotFoundException("Entity not found");
        repository.Remove(entity);
        await unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Minden entitás lekérdezése, opcionálisan a töröltekkel együtt.
    /// </summary>
    /// <param name="includeDeleted">Tartalmazza-e a törölt entitásokat.</param>
    /// <returns>Az entitások listája.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false)
    {
        return await repository.ListAsync(includeDeleted: includeDeleted);
    }
    
    /// <summary>
    /// Egy entitás lekérdezése azonosító alapján, opcionálisan a töröltekkel együtt.
    /// </summary>
    /// <param name="id">Az entitás azonosítója.</param>
    /// <param name="includeDeleted">Tartalmazza-e a törölt entitásokat.</param>
    /// <returns>Az entitás vagy null, ha nem található.</returns>
    public async Task<TEntity?> GetByIdAsync(int id, bool includeDeleted = false)
    {
        return await repository.FirstOrDefaultAsync(e => e.Id == id, includeDeleted);
    }
}