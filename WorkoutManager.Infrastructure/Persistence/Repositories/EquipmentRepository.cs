using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.Repositories;

internal sealed class EquipmentRepository(WorkoutDbContext db) :  EfRepository<Equipment>(db), IEquipmentRepository
{
    public async Task<List<Equipment>> GetByCategoryAsync(int categoryId, CancellationToken ct = default)
        => await _db.Equipment
            .Where(e => e.EquipmentCategoryId == categoryId)
            .ToListAsync(ct);
    
    public async Task<List<Equipment>> GetByContraindicationAsync(int contraindicationId, CancellationToken ct = default)
        => await _db.Equipment
            .Where(e => e.Contraindications.Any(c => c.Id == contraindicationId))
            .ToListAsync(ct);
}