using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;
using WorkoutManager.Shared.Constants;

namespace WorkoutManager.Infrastructure.Services;

public class EquipmentService(
    IEquipmentRepository equipmentRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService)
    : CrudService<Equipment>(equipmentRepository, unitOfWork), IEquipmentService
{
    public async Task<IEnumerable<Equipment>> GetEquipmentByCategoryAsync(int categoryId)
        => await equipmentRepository.GetByCategoryAsync(categoryId);
    
    /// <summary>
    /// Eszközök lekérdezése kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationId">A kontraindikáció azonosítója.</param>
    /// <returns>A kontraindikációval rendelkező eszközök listája.</returns>
    public async Task<IEnumerable<Equipment>> GetEquipmentByContraindicationAsync(int contraindicationId)
        => await equipmentRepository.GetByContraindicationAsync(contraindicationId);
    
    /// <summary>
    /// Eszközök lekérdezése több kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationIds">A kontraindikációk azonosítóinak listája.</param>
    /// <returns>A megadott kontraindikációkkal rendelkező eszközök listája.</returns>
    public async Task<IEnumerable<Equipment>> GetByContraindicationsAsync(List<int> contraindicationIds)
        => await equipmentRepository.GetByContraindicationsAsync(contraindicationIds);

    /// <summary>
    /// Minden eszköz lekérdezése, gyorsítótárazva (ha nincs törölt is kérve).
    /// </summary>
    /// <param name="includeDeleted">Tartalmazza-e a törölt eszközöket.</param>
    /// <returns>Az eszközök listája.</returns>
    public new async Task<IEnumerable<Equipment>> GetAllAsync(bool includeDeleted = false)
    {
        if (includeDeleted)
            return await base.GetAllAsync(includeDeleted);

        return await cacheService.GetOrCreateAsync(
            CacheKeyNames.AllEquipments,
            async () => await base.GetAllAsync(includeDeleted),
            absoluteExpireTime: TimeSpan.FromHours(1)
        ) ?? new List<Equipment>();
    }

    /// <summary>
    /// Új eszköz létrehozása, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="entity">A létrehozandó eszköz.</param>
    /// <returns>A létrehozott eszköz.</returns>
    public new async Task<Equipment> CreateAsync(Equipment entity)
    {
        var result = await base.CreateAsync(entity);
        cacheService.Remove(CacheKeyNames.AllEquipments);
        return result;
    }

    /// <summary>
    /// Eszköz frissítése, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="id">A frissítendő eszköz azonosítója.</param>
    /// <param name="entity">A frissített eszköz adatai.</param>
    /// <returns>A frissített eszköz.</returns>
    public new async Task<Equipment> UpdateAsync(int id, Equipment entity)
    {
        var result = await base.UpdateAsync(id, entity);
        cacheService.Remove(CacheKeyNames.AllEquipments);
        return result;
    }

    /// <summary>
    /// Eszköz törlése, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="id">A törlendő eszköz azonosítója.</param>
    public new async Task DeleteAsync(int id)
    {
        await base.DeleteAsync(id);
        cacheService.Remove(CacheKeyNames.AllEquipments);
    }
}