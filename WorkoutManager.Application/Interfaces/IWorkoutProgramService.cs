using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IWorkoutProgramService : ICrudService<WorkoutProgram>
{
    Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id);
}