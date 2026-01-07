using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Edzőeszközök kezelésére szolgáló szolgáltatás interfésze.
/// Lehetővé teszi az eszközök CRUD műveleteit, lekérdezést kategória és kontraindikáció alapján,
/// valamint több kontraindikáció szerinti szűrést.
/// </summary>
public interface IEquipmentService : ICrudService<Equipment>
{
    /// <summary>
    /// Eszközök lekérdezése kategória alapján.
    /// </summary>
    /// <param name="categoryId">A kategória azonosítója.</param>
    /// <returns>A kategóriához tartozó eszközök listája.</returns>
    Task<IEnumerable<Equipment>> GetEquipmentByCategoryAsync(int categoryId);

    /// <summary>
    /// Eszközök lekérdezése kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationId">A kontraindikáció azonosítója.</param>
    /// <returns>A kontraindikációval rendelkező eszközök listája.</returns>
    Task<IEnumerable<Equipment>> GetEquipmentByContraindicationAsync(int contraindicationId);
    
    /// <summary>
    /// Eszközök lekérdezése több kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationIds">A kontraindikációk azonosítóinak listája.</param>
    /// <returns>A megadott kontraindikációkkal rendelkező eszközök listája.</returns>
    Task<IEnumerable<Equipment>> GetByContraindicationsAsync(List<int> contraindicationIds);
}