using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Kontraindikációk kezelésére szolgáló szolgáltatás interfésze.
/// Lehetővé teszi a kontraindikációk CRUD műveleteit és lekérdezésüket azonosítók alapján.
/// </summary>
public interface IContraindicationService : ICrudService<Contraindication>
{
    /// <summary>
    /// Kontraindikációk lekérdezése azonosítók alapján.
    /// </summary>
    /// <param name="contraindicationIds">A kontraindikációk azonosítóinak listája.</param>
    /// <returns>A kontraindikációk listája.</returns>
    Task<IEnumerable<Contraindication>> GetAllByIdsAsync(List<int> contraindicationIds);
}