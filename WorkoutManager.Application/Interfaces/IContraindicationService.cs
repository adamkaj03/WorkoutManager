using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IContraindicationService
{
    Task<Contraindication> CreateContraindicationAsync(Contraindication contraindication);

    Task<Contraindication> UpdateContraindicationAsync(int id, Contraindication contraindication);

    Task DeleteContraindicationAsync(int id);

    Task<IEnumerable<Contraindication>> GetAllContraindicationsAsync();
}