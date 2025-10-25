using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ContraindicationService(IContraindicationRepository contraindicationRepository, IUnitOfWork unitOfWork)
    : CrudService<Contraindication>(contraindicationRepository, unitOfWork), IContraindicationService
{
    public async Task<IEnumerable<Contraindication>> GetAllByIdsAsync(List<int> contraindicationIds)
    {
        return await contraindicationRepository.ListAsync(c => contraindicationIds.Contains(c.Id));
    }
}