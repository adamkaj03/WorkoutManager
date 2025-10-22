using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.Repositories;

internal sealed class ContraindicationRepository(WorkoutDbContext db) : EfRepository<Contraindication>(db), IContraindicationRepository
{
    
}