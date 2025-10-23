using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ExerciseService(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork) : IExerciseService
{
    public Task<Exercise> CreateExerciseAsync(Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Task<Exercise> UpdateExerciseAsync(int id, Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Task DeleteExerciseAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
    {
        var entities = await exerciseRepository.ListAsync();
        return entities;
    }
}