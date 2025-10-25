using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;
using WorkoutManager.Shared.Exceptions;

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
        var existing = await repository.FirstOrDefaultAsync(e => e.Id == id);
        if (existing == null) throw new NotFoundException("Entity not found");
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await repository.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null) throw new NotFoundException("Entity not found");
        repository.Remove(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false)
    {
        return await repository.ListAsync(includeDeleted: includeDeleted);
    }
    
    public async Task<TEntity?> GetByIdAsync(int id, bool includeDeleted = false)
    {
        return await repository.FirstOrDefaultAsync(e => e.Id == id, includeDeleted);
    }
}