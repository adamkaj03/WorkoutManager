using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class WorkoutProgramService(IWorkoutProgramRepository workoutProgramRepository, IUnitOfWork unitOfWork)
    : CrudService<WorkoutProgram>(workoutProgramRepository, unitOfWork), IWorkoutProgramService
{ 
    public Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id)
    {
        var entity = workoutProgramRepository.GetFullWorkoutProgramAsync(id);
        return entity;
    }
}