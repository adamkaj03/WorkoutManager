using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Gyakorlatcsoportok kezelésére szolgáló szolgáltatás interfésze.
/// Lehetővé teszi a gyakorlatcsoportok CRUD műveleteit és több csoport lekérdezését azonosítók alapján.
/// </summary>
public interface IExerciseGroupService : ICrudService<ExerciseGroup>
{
    /// <summary>
    /// Gyakorlatcsoportok lekérdezése azonosítók alapján.
    /// </summary>
    /// <param name="exerciseGroupIds">A gyakorlatcsoportok azonosítóinak listája.</param>
    /// <returns>A gyakorlatcsoportok listája.</returns>
    Task<IEnumerable<ExerciseGroup>> GetAllByIdsAsync(List<int> exerciseGroupIds);
}