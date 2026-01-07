using Microsoft.EntityFrameworkCore.Storage;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Infrastructure.Persistence.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.UnitOfWork;

public class EfUnitOfWork(WorkoutDbContext db) : IUnitOfWork
{
    /// <summary>
    /// Minden függőben lévő módosítást elment az aktuális unit of work-ben az adatforrásba.
    /// </summary>
    /// <param name="ct">Megszakítási token (CancellationToken).</param>
    /// <returns>Az adatbázisba írt állapotbejegyzések száma.</returns>
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => db.SaveChangesAsync(ct);

    /// <summary>
    /// Visszaad egy repository példányt, amely ehhez az unit of work-höz tartozik.
    /// A repository ugyanazt a contextet/tranzakciót használja.
    /// </summary>
    /// <typeparam name="T">Az aggregátum gyökér típusa.</typeparam>
    public IRepository<T> GetRepository<T>() where T : BaseEntity
        => new EfRepository<T>(db);

    /// <summary>
    /// Adatbázis tranzakciót indít, és végrehajtja a megadott delegáltat benne.
    /// Ha a delegált sikeresen lefut, a tranzakció commitálódik, egyébként visszagörgetés történik (rollback).
    /// </summary>
    /// <param name="action">Aszinkron művelet, amelyet a tranzakción belül kell futtatni.</param>
    /// <param name="ct">Megszakítási token (CancellationToken).</param>
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