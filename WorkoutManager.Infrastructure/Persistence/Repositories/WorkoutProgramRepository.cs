using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.Repositories;

internal sealed class WorkoutProgramRepository(WorkoutDbContext db) : EfRepository<WorkoutProgram>(db), IWorkoutProgramRepository
{
    public async Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id, CancellationToken ct = default)
        => await _db.WorkoutPrograms
            .Include(wp => wp.ExerciseGroups)
                .ThenInclude(eg => eg.Exercises)
                    .ThenInclude(e => e.Contraindications)
            .FirstOrDefaultAsync(wp => wp.Id == id, ct);
}