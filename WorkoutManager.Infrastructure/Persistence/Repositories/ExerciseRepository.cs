using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.Repositories;

internal sealed class ExerciseRepository(WorkoutDbContext db) : EfRepository<Exercise>(db), IExerciseRepository
{
    public async Task<List<Exercise>> GetByContraindicationAsync(int contraindicationId, CancellationToken ct = default)
        => await _db.Exercises
            .Where(e => e.Contraindications.Any(c => c.Id == contraindicationId))
            .ToListAsync(ct);
}