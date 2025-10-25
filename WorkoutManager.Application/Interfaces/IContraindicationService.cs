using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IContraindicationService : ICrudService<Contraindication>
{
    Task<IEnumerable<Contraindication>> GetAllByIdsAsync(List<int> contraindicationIds);
}