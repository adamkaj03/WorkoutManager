using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IExerciseService : ICrudService<Exercise>
{
    Task<IEnumerable<Exercise>> GetExerciseByContraindicationAsync(int contraindicationId);
    Task AssignEquipmentAsync(int exerciseId, int equipmentId);
    Task AssignContraindicationsAsync(int exerciseId, List<int> contraindicationId);
}