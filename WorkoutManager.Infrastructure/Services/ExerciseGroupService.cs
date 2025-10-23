using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ExerciseGroupService(IExerciseGroupRepository exerciseGroupRepository, IUnitOfWork unitOfWork) 
    : IExerciseGroupService
{
    public Task<ExerciseGroup> CreateExerciseGroupAsync(ExerciseGroup exerciseGroup)
    {
        throw new NotImplementedException();
    }

    public Task<ExerciseGroup> UpdateExerciseGroupAsync(int id, ExerciseGroup exerciseGroup)
    {
        throw new NotImplementedException();
    }

    public Task DeleteExerciseGroupAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ExerciseGroup>> GetAllExerciseGroupsAsync()
    {
        var entities = await exerciseGroupRepository.ListAsync();
        return entities;
    }
}