using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class EquipmentService(IEquipmentRepository equipmentRepository, IUnitOfWork unitOfWork) 
    : CrudService<Equipment>(equipmentRepository, unitOfWork), IEquipmentService
{
    public async Task<IEnumerable<Equipment>> GetEquipmentByCategoryAsync(int categoryId)
        => await equipmentRepository.GetByCategoryAsync(categoryId);
    
    public async Task<IEnumerable<Equipment>> GetEquipmentByContraindicationAsync(int contraindicationId)
        => await equipmentRepository.GetByContraindicationAsync(contraindicationId);
}