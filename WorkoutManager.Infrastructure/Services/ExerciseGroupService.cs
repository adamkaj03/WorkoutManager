using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

/// <summary>
/// Gyakorlatcsoportok kezelésére szolgáló szolgáltatás service.
/// Lehetővé teszi a gyakorlatcsoportok CRUD műveleteit és több csoport lekérdezését azonosítók alapján.
/// </summary>
public class ExerciseGroupService(IExerciseGroupRepository exerciseGroupRepository, IUnitOfWork unitOfWork) 
    : CrudService<ExerciseGroup>(exerciseGroupRepository, unitOfWork), IExerciseGroupService
{
    /// <summary>
    /// Gyakorlatcsoportok lekérdezése azonosítók alapján.
    /// </summary>
    /// <param name="exerciseGroupIds">A gyakorlatcsoportok azonosítóinak listája.</param>
    /// <returns>A gyakorlatcsoportok listája.</returns>
    public async Task<IEnumerable<ExerciseGroup>> GetAllByIdsAsync(List<int> exerciseGroupIds)
    {
        return await exerciseGroupRepository.ListAsync(e => exerciseGroupIds.Contains(e.Id));
    }
}