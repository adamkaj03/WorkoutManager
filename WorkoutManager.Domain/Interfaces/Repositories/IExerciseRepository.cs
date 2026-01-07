using WorkoutManager.Models;

namespace WorkoutManager.Domain.Interfaces.Repositories;

public interface IExerciseRepository : IRepository<Exercise>
{
    Task<List<Exercise>> GetByContraindicationAsync(int contraindicationId, CancellationToken ct = default);
    Task<List<Exercise>> GetByContraindicationsAsync(List<int> contraindicationIds, CancellationToken ct = default);
}