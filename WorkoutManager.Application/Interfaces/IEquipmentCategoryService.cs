using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IEquipmentCategoryService
{
    Task<EquipmentCategory> CreateEquipmentCategoryAsync(EquipmentCategory equipmentCategory);

    Task<EquipmentCategory> UpdateEquipmentCategoryAsync(int id, EquipmentCategory equipmentCategory);

    Task DeleteEquipmentCategoryAsync(int id);

    Task<IEnumerable<EquipmentCategory>> GetAllEquipmentCategoriesAsync();
}