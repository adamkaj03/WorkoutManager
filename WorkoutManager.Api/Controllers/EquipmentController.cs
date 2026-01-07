using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

/// <summary>
/// Edzőeszközök kezelésére szolgáló controller.
/// Kezeli az eszközök CRUD műveleteit és speciális lekérdezéseket kategória és kontraindikáció alapján.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EquipmentController(IEquipmentService equipmentService, IMapper mapper) 
    : CrudController<Equipment, EquipmentDto>(equipmentService, mapper)
{
    /// <summary>
    /// Eszközök lekérdezése kategória alapján.
    /// 6. feladathoz tartozó endpoint.
    /// </summary>
    /// <param name="categoryId">A kategória azonosítója</param>
    /// <returns>A kategóriához tartozó eszközök listája</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    /// <response code="404">A kategória nem található</response>
    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetByCategory(int categoryId)
    {
        var equipments = await equipmentService.GetEquipmentByCategoryAsync(categoryId);
        var dtos = mapper.Map<IEnumerable<EquipmentDto>>(equipments);
        return Ok(dtos);
    }
    
    /// <summary>
    /// Eszközök lekérdezése kontraindikáció alapján.
    /// Visszaadja azokat az eszközöket, amelyek a megadott kontraindikációval rendelkeznek.
    /// 6. feladathoz tartozó endpoint.
    /// </summary>
    /// <param name="contraindicationId">A kontraindikáció azonosítója</param>
    /// <returns>A kontraindikációval rendelkező eszközök listája</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    /// <response code="404">A kontraindikáció nem található</response>
    [HttpGet("contraindication/{contraindicationId}")]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetByContraindication(int contraindicationId)
    {
        var equipments = await equipmentService.GetEquipmentByContraindicationAsync(contraindicationId);
        var dtos = mapper.Map<IEnumerable<EquipmentDto>>(equipments);
        return Ok(dtos);
    }
}
