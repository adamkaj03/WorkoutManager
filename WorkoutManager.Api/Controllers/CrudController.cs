using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

public abstract class CrudController<TEntity, TDto>(ICrudService<TEntity> service, IMapper mapper) : ControllerBase
    where TEntity : BaseEntity
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll([FromQuery] bool includeDeleted = false)
    {
        var entities = await service.GetAllAsync(includeDeleted);
        var dtos = mapper.Map<IEnumerable<TDto>>(entities);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> GetById(int id, [FromQuery] bool includeDeleted = false)
    {
        var entity = await service.GetByIdAsync(id, includeDeleted);
        if (entity == null)
            return NotFound(new { message = "Resource not found." });
        return Ok(mapper.Map<TDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<TDto>> Create([FromBody] TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        var created = await service.CreateAsync(entity);
        var createdDto = mapper.Map<TDto>(created);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, createdDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TDto>> Update(int id, [FromBody] TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        var updated = await service.UpdateAsync(id, entity);
        if (updated == null)
            return NotFound(new { message = "Resource not found." });
        var updatedDto = mapper.Map<TDto>(updated);
        return Ok(updatedDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}