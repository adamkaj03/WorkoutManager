using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

public abstract class CrudController<TEntity, TDto>(ICrudService<TEntity> service, IMapper mapper) : ControllerBase
    where TEntity : BaseEntity
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        var entities = await service.GetAllAsync();
        var dtos = mapper.Map<IEnumerable<TDto>>(entities);
        return Ok(dtos);
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