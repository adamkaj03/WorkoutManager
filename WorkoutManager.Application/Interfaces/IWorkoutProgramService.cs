using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IWorkoutProgramService
{
    Task<WorkoutProgram> CreateWorkoutProgramAsync(WorkoutProgram workoutProgram);
    
    Task<WorkoutProgram> UpdateWorkoutProgramAsync(int id, WorkoutProgram workoutProgram);
    
    Task DeleteWorkoutProgramAsync(int id);
    
    Task<IEnumerable<WorkoutProgram>> GetAllWorkoutProgramsAsync();
}