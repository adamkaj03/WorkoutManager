using WorkoutManager.Application.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Edzésprogramok kezelésére szolgáló szolgáltatás interfésze.
/// Lehetővé teszi az edzésprogramok CRUD műveleteit, teljes program lekérdezését,
/// gyakorlatcsoportok hozzárendelését, valamint cím és kontraindikáció lekérdezését gyakorlat alapján.
/// </summary>
public interface IWorkoutProgramService : ICrudService<WorkoutProgram>
{
    /// <summary>
    /// Egy teljes edzésprogram lekérdezése az összes kapcsolódó adattal.
    /// </summary>
    /// <param name="id">Az edzésprogram azonosítója.</param>
    /// <returns>Az edzésprogram összes kapcsolódó adattal, vagy null, ha nem található.</returns>
    Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id);

    /// <summary>
    /// Gyakorlatcsoportok hozzárendelése egy edzésprogramhoz.
    /// </summary>
    /// <param name="workoutProgramId">Az edzésprogram azonosítója.</param>
    /// <param name="exerciseGroupIds">A hozzárendelendő gyakorlatcsoportok azonosítói.</param>
    Task AssignExerciseGroupsAsync(int workoutProgramId, List<int> exerciseGroupIds);

    /// <summary>
    /// Edzésprogramok címének és kontraindikációinak lekérdezése egy adott gyakorlat alapján.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója.</param>
    /// <returns>Az edzésprogramok címe és kontraindikációi.</returns>
    Task<IEnumerable<WorkoutProgramWithContraindicationsDto>> GetAllTitleAndContraindicationByExerciseAsync(int exerciseId);

    /// <summary>
    /// Edzésprogramok lekérdezése azonosítók alapján.
    /// </summary>
    /// <param name="workoutProgramIds">Az edzésprogramok azonosítóinak listája.</param>
    /// <returns>Az edzésprogramok listája.</returns>
    Task<IEnumerable<WorkoutProgram>> GetAllByIdsAsync(List<int> workoutProgramIds);
}