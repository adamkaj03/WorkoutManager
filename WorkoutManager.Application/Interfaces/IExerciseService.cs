using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IExerciseService : ICrudService<Exercise>
{
    Task<IEnumerable<Exercise>> GetExerciseByContraindicationAsync(int contraindicationId);
}