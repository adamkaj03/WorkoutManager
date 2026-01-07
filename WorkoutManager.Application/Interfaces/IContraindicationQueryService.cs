using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

/**
 * Elkerülni a körkörös függőséget az IExerciseService és IEquipmentService között
 */
public interface IContraindicationQueryService
{
    Task<(IEnumerable<Equipment> Equipments, IEnumerable<Exercise> Exercises)> 
        GetItemsByContraindicationsAsync(List<int> contraindicationIds);
}