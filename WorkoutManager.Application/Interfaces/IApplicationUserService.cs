using WorkoutManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkoutManager.Application.Interfaces;

/// <summary>
/// Felhasználók kezelésére szolgáló szolgáltatás interfésze.
/// Lehetővé teszi a felhasználók lekérdezését, kedvenc edzésprogramok hozzáadását és törlését.
/// </summary>
public interface IApplicationUserService
{
    /// <summary>
    /// Felhasználó lekérdezése azonosító alapján.
    /// </summary>
    /// <param name="id">A felhasználó azonosítója.</param>
    /// <returns>A felhasználó entitás vagy kivétel, ha nem található.</returns>
    Task<User> GetByIdAsync(string id);

    /// <summary>
    /// Kedvenc edzésprogramok hozzáadása a felhasználóhoz.
    /// </summary>
    /// <param name="userId">A felhasználó azonosítója.</param>
    /// <param name="workoutProgramIds">A hozzáadandó edzésprogramok azonosítóinak listája.</param>
    Task AddFavouriteWorkoutProgramAsync(string userId, List<int> workoutProgramIds);

    /// <summary>
    /// Kedvenc edzésprogramok eltávolítása a felhasználótól.
    /// </summary>
    /// <param name="userId">A felhasználó azonosítója.</param>
    /// <param name="workoutProgramIds">A törlendő edzésprogramok azonosítóinak listája.</param>
    Task DeleteFavouriteWorkoutProgramAsync(string userId, List<int> workoutProgramIds);
}