using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;
using WorkoutManager.Shared.Constants;

namespace WorkoutManager.Infrastructure.Services;

public class ContraindicationService(
    IContraindicationRepository contraindicationRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService)
    : CrudService<Contraindication>(contraindicationRepository, unitOfWork), IContraindicationService
{
    public async Task<IEnumerable<Contraindication>> GetAllByIdsAsync(List<int> contraindicationIds)
    {
        return await contraindicationRepository.ListAsync(c => contraindicationIds.Contains(c.Id));
    }

    /// <summary>
    /// Minden kontraindikáció lekérdezése, gyorsítótárazva (ha nincs törölt is kérve).
    /// </summary>
    /// <param name="includeDeleted">Tartalmazza-e a törölt kontraindikációkat.</param>
    /// <returns>A kontraindikációk listája.</returns>
    public new async Task<IEnumerable<Contraindication>> GetAllAsync(bool includeDeleted = false)
    {
        if (includeDeleted)
            return await base.GetAllAsync(includeDeleted);

        return await cacheService.GetOrCreateAsync(
            CacheKeyNames.AllContraindications,
            async () => await base.GetAllAsync(includeDeleted),
            absoluteExpireTime: TimeSpan.FromHours(1)
        ) ?? new List<Contraindication>();
    }

    /// <summary>
    /// Új kontraindikáció létrehozása, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="entity">A létrehozandó kontraindikáció.</param>
    /// <returns>A létrehozott kontraindikáció.</returns>
    public new async Task<Contraindication> CreateAsync(Contraindication entity)
    {
        var result = await base.CreateAsync(entity);
        cacheService.Remove(CacheKeyNames.AllContraindications);
        return result;
    }

    /// <summary>
    /// Kontraindikáció frissítése, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="id">A frissítendő kontraindikáció azonosítója.</param>
    /// <param name="entity">A frissített kontraindikáció adatai.</param>
    /// <returns>A frissített kontraindikáció.</returns>
    public new async Task<Contraindication> UpdateAsync(int id, Contraindication entity)
    {
        var result = await base.UpdateAsync(id, entity);
        cacheService.Remove(CacheKeyNames.AllContraindications);
        return result;
    }

    /// <summary>
    /// Kontraindikáció törlése, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="id">A törlendő kontraindikáció azonosítója.</param>
    public new async Task DeleteAsync(int id)
    {
        await base.DeleteAsync(id);
        cacheService.Remove(CacheKeyNames.AllContraindications);
    }
}