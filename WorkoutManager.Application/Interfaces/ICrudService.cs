namespace WorkoutManager.Application.Interfaces;

public interface ICrudService<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(int id, TEntity entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false);
    Task<TEntity?> GetByIdAsync(int id, bool includeDeleted = false);
}