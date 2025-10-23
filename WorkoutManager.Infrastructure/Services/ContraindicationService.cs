using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ContraindicationService(IContraindicationRepository contraindicationRepository, IUnitOfWork unitOfWork) 
    : IContraindicationService
{
    public Task<Contraindication> CreateContraindicationAsync(Contraindication contraindication)
    {
        throw new NotImplementedException();
    }

    public Task<Contraindication> UpdateContraindicationAsync(int id, Contraindication contraindication)
    {
        throw new NotImplementedException();
    }

    public Task DeleteContraindicationAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Contraindication>> GetAllContraindicationsAsync()
    {
        var entities = await contraindicationRepository.ListAsync();
        return entities;
    }
}