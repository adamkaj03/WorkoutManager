using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class CrudService<TEntity>(IRepository<TEntity> repository, IUnitOfWork unitOfWork) : ICrudService<TEntity>
    where TEntity : BaseEntity
{

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null) throw new Exception("Entity not found");
        // Add property copying logic or use AutoMapper if needed
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new Exception("Entity not found");
        repository.Remove(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await repository.ListAsync();
    }
}