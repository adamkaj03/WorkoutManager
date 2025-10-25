using System.Linq.Expressions;
using WorkoutManager.Models;

namespace WorkoutManager.Domain.Interfaces.Repositories;

/**
 * Az interface-t a példában megadott projektből vettem át.
 */
/// <summary>
/// Általános repository interfész, amely entitások kezelését biztosítja.
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Visszaad egy lekérdezhető forrást (IQueryable), amely tovább szűrhető.
    /// Ajánlott csak olvasási műveleteket használni, ne materializáld az egész halmazt egyszerre.
    /// </summary>
    IQueryable<T> AsQueryable();

    /// <summary>
    /// Egyetlen entitást keres, amely megfelel a megadott feltételnek (predicate), vagy <c>null</c>-t ad vissza.
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool includeDeleted = false, CancellationToken ct = default);

    /// <summary>
    /// Minden entitást visszaad, amely megfelel a megadott feltételnek.
    /// Ha a <paramref name="predicate"/> <c>null</c>, akkor az összes entitást visszaadja.
    /// </summary>
    Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, bool includeDeleted = false, CancellationToken ct = default);

    /// <summary>
    /// Megvizsgálja, hogy van-e olyan entitás, amely megfelel a megadott feltételnek.
    /// Ha a <paramref name="predicate"/> <c>null</c>, akkor azt ellenőrzi, hogy létezik-e bármilyen entitás.
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

    /// <summary>
    /// Megszámolja, hány entitás felel meg a megadott feltételnek.
    /// Ha a <paramref name="predicate"/> <c>null</c>, akkor az összeset megszámolja.
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

    /// <summary>
    /// Új entitást ad hozzá az adattárhoz (nem kerül végleges mentésre, amíg a UnitOfWork.SaveChangesAsync nem hívódik).
    /// </summary>
    Task AddAsync(T entity, CancellationToken ct = default);

    /// <summary>
    /// Több entitást ad hozzá egyszerre az adattárhoz (nem kerülnek véglegesen mentésre, amíg a UnitOfWork.SaveChangesAsync nem hívódik).
    /// </summary>
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

    /// <summary>
    /// Egy entitást frissít. Az implementáció szükség esetén csatolja az entitást és módosítottnak jelöli.
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Több entitást frissít egyszerre.
    /// </summary>
    void UpdateRange(IEnumerable<T> entities);

    /// <summary>
    /// Egy entitást eltávolít.
    /// </summary>
    void Remove(T entity);

    /// <summary>
    /// Több entitást távolít el egyszerre.
    /// </summary>
    void RemoveRange(IEnumerable<T> entities);
}