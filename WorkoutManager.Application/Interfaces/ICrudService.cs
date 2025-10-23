namespace WorkoutManager.Application.Interfaces;

public interface ICrudService<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(int id, TEntity entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}