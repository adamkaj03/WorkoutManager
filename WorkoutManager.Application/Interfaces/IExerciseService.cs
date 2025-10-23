using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IExerciseService
{
    Task<Exercise> CreateExerciseAsync(Exercise exercise);

    Task<Exercise> UpdateExerciseAsync(int id, Exercise exercise);

    Task DeleteExerciseAsync(int id);

    Task<IEnumerable<Exercise>> GetAllExercisesAsync();
}