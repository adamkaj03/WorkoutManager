using System.Linq;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;
using WorkoutManager.Shared.Exceptions;

namespace WorkoutManager.Infrastructure.Services;

/// <summary>
/// Felhasználók kezelésére szolgáló szolgáltatás.
/// Lehetővé teszi a felhasználók lekérdezését, kedvenc edzésprogramok hozzáadását és törlését.
/// </summary>
public class ApplicationUserService(IApplicationUserRepository repository,
    IUnitOfWork unitOfWork, IWorkoutProgramService workoutProgramService)
    : IApplicationUserService
{
    public async Task<User> GetByIdAsync(string id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity ?? throw new NotFoundException("Workout program not found");
    }
    
    /// <summary>
    /// Kedvenc edzésprogramok hozzáadása a felhasználóhoz.
    /// </summary>
    /// <param name="userId">A felhasználó azonosítója.</param>
    /// <param name="workoutProgramIds">A hozzáadandó edzésprogramok azonosítóinak listája.</param>
    public async Task AddFavouriteWorkoutProgramAsync(string userId, List<int> workoutProgramIds)
    {
        var user = await repository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found");

        var workoutPrograms = await workoutProgramService.GetAllByIdsAsync(workoutProgramIds);

        foreach (var program in workoutPrograms)
        {
            if (user.WorkoutPrograms.All(wp => wp.Id != program.Id))
            {
                user.WorkoutPrograms.Add(program);
            }
        }

        repository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Kedvenc edzésprogramok eltávolítása a felhasználótól.
    /// </summary>
    /// <param name="userId">A felhasználó azonosítója.</param>
    /// <param name="workoutProgramIds">A törlendő edzésprogramok azonosítóinak listája.</param>
    public async Task DeleteFavouriteWorkoutProgramAsync(string userId, List<int> workoutProgramIds)
    {
        var user = await repository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found");

        var workoutPrograms = await workoutProgramService.GetAllByIdsAsync(workoutProgramIds);

        // Biztonság: ha a navigációs gyűjtemény null, inicializáljuk
        if (user.WorkoutPrograms == null)
            user.WorkoutPrograms = new List<WorkoutProgram>();

        // Eltávolítjuk azokat a programokat, amelyek szerepelnek a lekérdezés eredményében
        foreach (var program in workoutPrograms)
        {
            var existing = user.WorkoutPrograms.FirstOrDefault(wp => wp.Id == program.Id);
            if (existing != null)
            {
                user.WorkoutPrograms.Remove(existing);
            }
        }

        repository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }
}