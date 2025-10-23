using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IExerciseGroupService
{
    Task<ExerciseGroup> CreateExerciseGroupAsync(ExerciseGroup exerciseGroup);

    Task<ExerciseGroup> UpdateExerciseGroupAsync(int id, ExerciseGroup exerciseGroup);

    Task DeleteExerciseGroupAsync(int id);

    Task<IEnumerable<ExerciseGroup>> GetAllExerciseGroupsAsync();
}