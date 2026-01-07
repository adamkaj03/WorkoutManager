using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

/// <summary>
/// Kontraindikációk kezelésére szolgáló controller.
/// Kezeli a kontraindikációk CRUD műveleteit és a hozzájuk kapcsolódó eszközök/gyakorlatok lekérdezését.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContraindicationsController(
    IContraindicationService contraindicationService,
    IContraindicationQueryService queryService,
    IMapper mapper) 
    : CrudController<Contraindication, ContraindicationDto>(contraindicationService, mapper)
{
    /// <summary>
    /// Az összes kontraindikáció lekérdezése.
    /// Mindenki számára elérhető (nincs autentikáció szükséges).
    /// </summary>
    /// <param name="includeDeleted">Töröltek megjelenítése (soft delete)</param>
    /// <returns>Kontraindikációk listája</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    [HttpGet]
    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<ContraindicationDto>>> GetAll([FromQuery] bool includeDeleted = false)
    {
        return await base.GetAll(includeDeleted);
    }
    
    /// <summary>
    /// Eszközök és gyakorlatok lekérdezése kontraindikációk alapján.
    /// Visszaadja azokat az eszközöket és gyakorlatokat, amelyek a megadott kontraindikációkkal rendelkeznek.
    /// </summary>
    /// <param name="contraindicationIds">A kontraindikációk azonosítóinak listája</param>
    /// <returns>Eszközök és gyakorlatok listája</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör</response>
    [HttpPost("equipment-exercises")]
    [Authorize(Roles = "Writer,Reader")]
    public async Task<ActionResult<object>> GetEquipmentAndExercisesByContraindications(
        [FromBody] List<int> contraindicationIds)
    {
        var (equipments, exercises) = await queryService.GetItemsByContraindicationsAsync(contraindicationIds);

        var result = new
        {
            Equipments = mapper.Map<IEnumerable<EquipmentDto>>(equipments),
            Exercises = mapper.Map<IEnumerable<ExerciseDto>>(exercises)
        };

        return Ok(result);
    }
}
