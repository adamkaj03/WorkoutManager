using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ExerciseGroupService(IExerciseGroupRepository exerciseGroupRepository, IUnitOfWork unitOfWork) 
    : CrudService<ExerciseGroup>(exerciseGroupRepository, unitOfWork), IExerciseGroupService
{
    public async Task<IEnumerable<ExerciseGroup>> GetAllByIdsAsync(List<int> exerciseGroupIds)
    {
        return await exerciseGroupRepository.ListAsync(e => exerciseGroupIds.Contains(e.Id));
    }
}