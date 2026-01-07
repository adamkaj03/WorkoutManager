using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Gyakorlatok kezelésére szolgáló szolgáltatás interfésze.
/// Lehetővé teszi a gyakorlatok CRUD műveleteit, eszköz és kontraindikáció hozzárendelését,
/// valamint lekérdezést kontraindikációk alapján.
/// </summary>
public interface IExerciseService : ICrudService<Exercise>
{
    /// <summary>
    /// Gyakorlatok lekérdezése egy adott kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationId">A kontraindikáció azonosítója.</param>
    /// <returns>A megadott kontraindikációval rendelkező gyakorlatok listája.</returns>
    Task<IEnumerable<Exercise>> GetExerciseByContraindicationAsync(int contraindicationId);

    /// <summary>
    /// Gyakorlatok lekérdezése több kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationIds">A kontraindikációk azonosítóinak listája.</param>
    /// <returns>A megadott kontraindikációkkal rendelkező gyakorlatok listája.</returns>
    Task<IEnumerable<Exercise>> GetByContraindicationsAsync(List<int> contraindicationIds);

    /// <summary>
    /// Eszköz hozzárendelése egy gyakorlathoz.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója.</param>
    /// <param name="equipmentId">A hozzárendelendő eszköz azonosítója.</param>
    Task AssignEquipmentAsync(int exerciseId, int equipmentId);

    /// <summary>
    /// Kontraindikációk hozzárendelése egy gyakorlathoz.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója.</param>
    /// <param name="contraindicationId">A hozzárendelendő kontraindikációk azonosítóinak listája.</param>
    Task AssignContraindicationsAsync(int exerciseId, List<int> contraindicationId);
}