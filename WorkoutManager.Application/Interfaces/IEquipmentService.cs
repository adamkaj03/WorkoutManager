using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IEquipmentService
{
    Task<Equipment> CreateEquipmentAsync(Equipment equipment);

    Task<Equipment> UpdateEquipmentAsync(int id, Equipment equipment);

    Task DeleteEquipmentAsync(int id);

    Task<IEnumerable<Equipment>> GetAllEquipmentAsync();
}