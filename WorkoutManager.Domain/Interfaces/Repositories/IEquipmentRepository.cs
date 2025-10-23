using WorkoutManager.Models;

namespace WorkoutManager.Domain.Interfaces.Repositories;

public interface IEquipmentRepository : IRepository<Equipment>
{
    Task<List<Equipment>> GetByCategoryAsync(int categoryId, CancellationToken ct = default);

    Task<List<Equipment>> GetByContraindicationAsync(int contraindicationId, CancellationToken ct = default);
}