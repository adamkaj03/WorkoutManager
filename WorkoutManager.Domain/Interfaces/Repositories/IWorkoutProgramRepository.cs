using WorkoutManager.Models;

namespace WorkoutManager.Domain.Interfaces.Repositories;

public interface IWorkoutProgramRepository : IRepository<WorkoutProgram>
{
    Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<WorkoutProgram>> GetAllTitleAndContraindicationByExerciseAsync(int exerciseId);
}