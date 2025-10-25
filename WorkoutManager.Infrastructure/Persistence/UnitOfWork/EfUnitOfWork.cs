using Microsoft.EntityFrameworkCore.Storage;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Infrastructure.Persistence.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.UnitOfWork;

public class EfUnitOfWork(WorkoutDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => db.SaveChangesAsync(ct);

    public IRepository<T> GetRepository<T>() where T : BaseEntity
        => new EfRepository<T>(db);

    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken ct = default)
    {
        // If already in a transaction (e.g., ambient), reuse it.
        if (db.Database.CurrentTransaction is not null)
        {
            await action();
            await db.SaveChangesAsync(ct);
            return;
        }

        await using IDbContextTransaction tx = await db.Database.BeginTransactionAsync(ct);
        try
        {
            await action();
            await db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }
    }

    public ValueTask DisposeAsync() => db.DisposeAsync();
}