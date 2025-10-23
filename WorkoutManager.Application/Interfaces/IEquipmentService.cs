using WorkoutManager.Models;

namespace WorkoutManager.Application.Interfaces;

public interface IEquipmentService : ICrudService<Equipment>
{
    Task<IEnumerable<Equipment>> GetEquipmentByCategoryAsync(int categoryId);

    Task<IEnumerable<Equipment>> GetEquipmentByContraindicationAsync(int contraindicationId);
}