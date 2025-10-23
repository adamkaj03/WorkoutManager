using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class EquipmentCategoryService(IEquipmentCategoryRepository equipmentCategoryRepository, IUnitOfWork unitOfWork) 
    : IEquipmentCategoryService
{
    public Task<EquipmentCategory> CreateEquipmentCategoryAsync(EquipmentCategory equipmentCategory)
    {
        throw new NotImplementedException();
    }

    public Task<EquipmentCategory> UpdateEquipmentCategoryAsync(int id, EquipmentCategory equipmentCategory)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEquipmentCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<EquipmentCategory>> GetAllEquipmentCategoriesAsync()
    {
        var entities = await equipmentCategoryRepository.ListAsync();
        return entities;
    }
}