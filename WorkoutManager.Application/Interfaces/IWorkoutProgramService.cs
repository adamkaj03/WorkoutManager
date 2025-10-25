using WorkoutManager.Application.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IWorkoutProgramService : ICrudService<WorkoutProgram>
{
    Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id);
    Task AssignExerciseGroupsAsync(int workoutProgramId, List<int> exerciseGroupIds);
    Task<IEnumerable<WorkoutProgramWithContraindicationsDto>> GetAllTitleAndContraindicationByExerciseAsync(int exerciseId);
}