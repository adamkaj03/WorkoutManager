using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class EquipmentService(IEquipmentRepository equipmentRepository, IUnitOfWork unitOfWork) 
    : IEquipmentService
{
    public Task<Equipment> CreateEquipmentAsync(Equipment equipment)
    {
        throw new NotImplementedException();
    }

    public Task<Equipment> UpdateEquipmentAsync(int id, Equipment equipment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEquipmentAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync()
    {
        var entities = await equipmentRepository.ListAsync();
        return entities;
    }
}