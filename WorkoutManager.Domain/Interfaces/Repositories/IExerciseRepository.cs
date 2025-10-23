using WorkoutManager.Models;

namespace WorkoutManager.Domain.Interfaces.Repositories;

public interface IExerciseRepository : IRepository<Exercise>
{
    Task<List<Exercise>> GetByContraindicationAsync(int contraindicationId, CancellationToken ct = default);
}