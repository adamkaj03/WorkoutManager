using WorkoutManager.Application.Interfaces;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ContraindicationQueryService(
    IEquipmentService equipmentService,
    IExerciseService exerciseService) 
    : IContraindicationQueryService
{
    public async Task<(IEnumerable<Equipment> Equipments, IEnumerable<Exercise> Exercises)> 
        GetItemsByContraindicationsAsync(List<int> contraindicationIds)
    {
        var equipments = await equipmentService.GetByContraindicationsAsync(contraindicationIds);
        var exercises = await exerciseService.GetByContraindicationsAsync(contraindicationIds);
        
        return (equipments, exercises);
    }
}
