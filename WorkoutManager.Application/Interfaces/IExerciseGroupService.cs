using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IExerciseGroupService : ICrudService<ExerciseGroup>
{
    Task<IEnumerable<ExerciseGroup>> GetAllByIdsAsync(List<int> exerciseGroupIds);
}