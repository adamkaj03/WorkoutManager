using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Domain.Interfaces;

/// <summary>
/// Tranzakciós műveletek koordinálása több repository között.
/// EF Core-ban a DbContext már unit of work-ként működik, de ez az absztrakció
/// hasznos teszteléshez, folyamatok szervezéséhez, illetve hogy a felsőbb rétegek ne hivatkozzanak közvetlenül az EF Core-ra.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
    /// <summary>
    /// Minden függőben lévő módosítást elment az aktuális unit of work-ben az adatforrásba.
    /// </summary>
    /// <param name="ct">Megszakítási token (CancellationToken).</param>
    /// <returns>Az adatbázisba írt állapotbejegyzések száma.</returns>
    Task<int> SaveChangesAsync(CancellationToken ct = default);

    /// <summary>
    /// Visszaad egy repository példányt, amely ehhez az unit of work-höz tartozik.
    /// A repository ugyanazt a contextet/tranzakciót használja.
    /// </summary>
    /// <typeparam name="T">Az aggregátum gyökér típusa.</typeparam>
    IRepository<T> GetRepository<T>() where T : BaseEntity;

    /// <summary>
    /// Adatbázis tranzakciót indít, és végrehajtja a megadott delegáltat benne.
    /// Ha a delegált sikeresen lefut, a tranzakció commitálódik, egyébként visszagörgetés történik (rollback).
    /// </summary>
    /// <param name="action">Aszinkron művelet, amelyet a tranzakción belül kell futtatni.</param>
    /// <param name="ct">Megszakítási token (CancellationToken).</param>
    Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken ct = default);
}