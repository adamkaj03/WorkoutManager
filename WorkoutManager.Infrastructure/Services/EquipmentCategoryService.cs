using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class EquipmentCategoryService(IEquipmentCategoryRepository equipmentCategoryRepository, IUnitOfWork unitOfWork) 
    : CrudService<EquipmentCategory>(equipmentCategoryRepository, unitOfWork), IEquipmentCategoryService
{
}