using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

/// <summary>
/// Gyakorlatok kezelésére szolgáló controller.
/// Kezeli a gyakorlatok CRUD műveleteit, eszköz és kontraindikáció hozzárendelését.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExercisesController(IExerciseService exerciseService, IMapper mapper) 
    : CrudController<Exercise, ExerciseDto>(exerciseService, mapper)
{
    /// <summary>
    /// Gyakorlatok lekérdezése kontraindikáció alapján.
    /// Visszaadja azokat a gyakorlatokat, amelyek a megadott kontraindikációval rendelkeznek.
    /// 6. feladathoz tartozó endpoint.
    /// </summary>
    /// <param name="contraindicationId">A kontraindikáció azonosítója</param>
    /// <returns>A kontraindikációval rendelkező gyakorlatok listája</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    /// <response code="404">A kontraindikáció nem található</response>
    [HttpGet("contraindication/{contraindicationId}")]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetByContraindication(int contraindicationId)
    {
        var entities = await exerciseService.GetExerciseByContraindicationAsync(contraindicationId);
        var dtos = mapper.Map<IEnumerable<ExerciseDto>>(entities);
        return Ok(dtos);
    }
    
    /// <summary>
    /// Eszköz hozzárendelése egy gyakorlathoz.
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója</param>
    /// <param name="equipmentId">Az eszköz azonosítója</param>
    /// <returns>NoContent válasz sikeres hozzárendelés esetén</returns>
    /// <response code="204">Sikeres hozzárendelés</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    /// <response code="404">A gyakorlat vagy eszköz nem található</response>
    [HttpPost("{exerciseId}/equipment/{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignEquipmentToExercise(int exerciseId, int equipmentId)
    {
        await exerciseService.AssignEquipmentAsync(exerciseId, equipmentId);
        return NoContent();
    }

    /// <summary>
    /// Kontraindikációk hozzárendelése egy gyakorlathoz.
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója</param>
    /// <param name="contraindicationIds">A kontraindikációk azonosítóinak listája</param>
    /// <returns>NoContent válasz sikeres hozzárendelés esetén</returns>
    /// <response code="204">Sikeres hozzárendelés</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    /// <response code="404">A gyakorlat vagy kontraindikáció nem található</response>
    [HttpPost("{exerciseId}/contraindications")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignContraindicationsToExercise(
        int exerciseId,
        [FromBody] List<int> contraindicationIds)
    {
        await exerciseService.AssignContraindicationsAsync(exerciseId, contraindicationIds);
        return NoContent();
    }
}
