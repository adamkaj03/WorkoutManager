using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Entities;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

/// <summary>
/// Általános CRUD műveletek kezelésére szolgáló absztrakt controller.
/// Alapvető létrehozás, olvasás, frissítés és törlés műveleteket biztosít minden entitás számára.
/// </summary>
/// <typeparam name="TEntity">Az entitás típusa (BaseEntity leszármazott)</typeparam>
/// <typeparam name="TDto">A Data Transfer Object típusa</typeparam>
public abstract class CrudController<TEntity, TDto>(ICrudService<TEntity> service, IMapper mapper) : ControllerBase
    where TEntity : BaseEntity
{
    /// <summary>
    /// Az összes entitás lekérdezése.
    /// </summary>
    /// <param name="includeDeleted">Töröltek megjelenítése (soft delete)</param>
    /// <returns>Entitások listája DTO formátumban</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll([FromQuery] bool includeDeleted = false)
    {
        var entities = await service.GetAllAsync(includeDeleted);
        var dtos = mapper.Map<IEnumerable<TDto>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Egy entitás lekérdezése azonosító alapján.
    /// </summary>
    /// <param name="id">Az entitás azonosítója</param>
    /// <param name="includeDeleted">Töröltek között is keressen</param>
    /// <returns>Az entitás DTO formátumban</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    /// <response code="404">Az entitás nem található</response>
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDto>> GetById(int id, [FromQuery] bool includeDeleted = false)
    {
        var entity = await service.GetByIdAsync(id, includeDeleted);
        if (entity == null)
            return NotFound(new { message = "Resource not found." });
        return Ok(mapper.Map<TDto>(entity));
    }

    /// <summary>
    /// Új entitás létrehozása.
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="dto">A létrehozandó entitás adatai DTO formátumban</param>
    /// <returns>A létrehozott entitás DTO formátumban</returns>
    /// <response code="201">Sikeres létrehozás</response>
    /// <response code="400">Hibás adatok</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public virtual  async Task<ActionResult<TDto>> Create([FromBody] TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        var created = await service.CreateAsync(entity);
        var createdDto = mapper.Map<TDto>(created);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, createdDto);
    }

    /// <summary>
    /// Meglévő entitás frissítése.
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="id">A frissítendő entitás azonosítója</param>
    /// <param name="dto">Az új adatok DTO formátumban</param>
    /// <returns>A frissített entitás DTO formátumban</returns>
    /// <response code="200">Sikeres frissítés</response>
    /// <response code="400">Hibás adatok</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    /// <response code="404">Az entitás nem található</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public virtual async Task<ActionResult<TDto>> Update(int id, [FromBody] TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        var updated = await service.UpdateAsync(id, entity);
        if (updated == null)
            return NotFound(new { message = "Resource not found." });
        var updatedDto = mapper.Map<TDto>(updated);
        return Ok(updatedDto);
    }

    /// <summary>
    /// Entitás törlése (soft delete).
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="id">A törlendő entitás azonosítója</param>
    /// <returns>NoContent válasz sikeres törlés esetén</returns>
    /// <response code="204">Sikeres törlés</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    /// <response code="404">Az entitás nem található</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}