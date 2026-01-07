using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.Repositories;

/**
 * Az osztályt a példában megadott projektből vettem át.
 */
internal class EfRepository<T>(WorkoutDbContext db) : IRepository<T> where T : class
{
    protected readonly WorkoutDbContext _db = db;
    protected readonly DbSet<T> _set = db.Set<T>();
    
    public async Task<List<T>> GetAllAsync(CancellationToken ct = default)
        => await _set.ToListAsync(ct);

    public IQueryable<T> AsQueryable() => _set.AsQueryable();

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool includeDeleted = false, CancellationToken ct = default)
    {
        IQueryable<T> query = _set;

        if (includeDeleted)
            query = query.IgnoreQueryFilters();

        return query.FirstOrDefaultAsync(predicate, ct);
    }

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, bool includeDeleted = false, CancellationToken ct = default)
    {
        IQueryable<T> query = _set;

        if (includeDeleted)
            query = query.IgnoreQueryFilters();

        if (predicate != null)
            query = query.Where(predicate);

        return await query.ToListAsync(ct);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
        => predicate is null ? _set.AnyAsync(ct) : _set.AnyAsync(predicate, ct);

    public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
        => predicate is null ? _set.CountAsync(ct) : _set.CountAsync(predicate, ct);

    public Task AddAsync(T entity, CancellationToken ct = default) => _set.AddAsync(entity, ct).AsTask();
    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default) => _set.AddRangeAsync(entities, ct);

    public void Update(T entity) => _set.Update(entity);
    public void UpdateRange(IEnumerable<T> entities) => _set.UpdateRange(entities);

    
    // 7. feladathoz tartozó soft-delete
    public void Remove(T entity)
    {
        var baseEntity = entity as BaseEntity;
        if (baseEntity != null)
        {
            baseEntity.IsDeleted = true;
            _set.Update(entity);
        }
        else
        {
            _set.Remove(entity);
        }
        _set.Update(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Remove(entity);
        }
    }
}