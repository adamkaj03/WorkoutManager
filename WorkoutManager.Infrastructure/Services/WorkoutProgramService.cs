using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class WorkoutProgramService(IWorkoutProgramRepository workoutProgramRepository, IUnitOfWork unitOfWork)
    : IWorkoutProgramService
{
    public Task<WorkoutProgram> CreateWorkoutProgramAsync(WorkoutProgram workoutProgram)
    {
        throw new NotImplementedException();
    }

    public Task<WorkoutProgram> UpdateWorkoutProgramAsync(int id, WorkoutProgram workoutProgram)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWorkoutProgramAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<WorkoutProgram>> GetAllWorkoutProgramsAsync()
    {
        var entities = await workoutProgramRepository.ListAsync();
        return entities;
    }
}