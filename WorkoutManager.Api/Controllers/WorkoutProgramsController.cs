using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

/// <summary>
/// Edzésprogramok kezelésére szolgáló controller.
/// Kezeli az edzésprogramok CRUD műveleteit, gyakorlatcsoportok hozzárendelését és kontraindikációk lekérdezését.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkoutProgramsController(IWorkoutProgramService workoutProgramService, IMapper mapper)
    : CrudController<WorkoutProgram, WorkoutProgramDto>(workoutProgramService, mapper)
{
    /// <summary>
    /// Az összes edzésprogram lekérdezése.
    /// Mindenki számára elérhető (nincs autentikáció szükséges).
    /// </summary>
    /// <param name="includeDeleted">Töröltek megjelenítése (soft delete)</param>
    /// <returns>Edzésprogramok listája</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    [AllowAnonymous]
    public override Task<ActionResult<IEnumerable<WorkoutProgramDto>>> GetAll(bool includeDeleted = false)
    {
        return base.GetAll(includeDeleted);
    }

    /// <summary>
    /// Teljes edzésprogram lekérdezése minden kapcsolódó adattal.
    /// Tartalmazza a gyakorlatcsoportokat, gyakorlatokat, eszközöket és kontraindikációkat.
    /// 5. feladathoz tartozó végpont.
    /// </summary>
    /// <param name="id">Az edzésprogram azonosítója</param>
    /// <returns>A teljes edzésprogram adatai</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    /// <response code="404">Az edzésprogram nem található</response>
    [HttpGet("full/{id}")]
    public async Task<ActionResult<WorkoutProgramDto>> GetFull(int id)
    {
        var entity = await workoutProgramService.GetFullWorkoutProgramAsync(id);
        if (entity == null)
            return NotFound();

        var dto = mapper.Map<WorkoutProgramDto>(entity);
        return Ok(dto);
    }
    
    /// <summary>
    /// Gyakorlatcsoportok hozzárendelése egy edzésprogramhoz.
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="workoutProgramId">Az edzésprogram azonosítója</param>
    /// <param name="exerciseGroupIds">A gyakorlatcsoportok azonosítóinak listája</param>
    /// <returns>NoContent válasz sikeres hozzárendelés esetén</returns>
    /// <response code="204">Sikeres hozzárendelés</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    /// <response code="404">Az edzésprogram vagy gyakorlatcsoport nem található</response>
    [HttpPost("{workoutProgramId}/exercise-groups")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignExerciseGroupsToWorkoutPrograms(
        int workoutProgramId,
        [FromBody] List<int> exerciseGroupIds)
    {
        await workoutProgramService.AssignExerciseGroupsAsync(workoutProgramId, exerciseGroupIds);
        return NoContent();
    }
    
    /// <summary>
    /// Edzésprogramok és kontraindikációk lekérdezése gyakorlat alapján.
    /// Visszaadja azokat az edzésprogramokat és azok kontraindikációit, amelyek a megadott gyakorlatot tartalmazzák.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója</param>
    /// <returns>Edzésprogramok címeivel és kontraindikációival</returns>
    /// <response code="200">Sikeres lekérdezés</response>
    /// <response code="404">A gyakorlat nem található</response>
    [HttpGet("by-exercise/{exerciseId}/contraindications")]
    public async Task<ActionResult<IEnumerable<WorkoutProgramWithContraindicationsDto>>> GetAllTitleAndContraindicationByExerciseAsync(
        int exerciseId)
    {
        var dtos = await workoutProgramService.GetAllTitleAndContraindicationByExerciseAsync(exerciseId);
        return Ok(dtos);
    }

    /// <summary>
    /// Új edzésprogram létrehozása.
    /// Adminisztrátorok és Writer szerepkörű felhasználók használhatják.
    /// </summary>
    /// <param name="dto">A létrehozandó edzésprogram adatai</param>
    /// <returns>A létrehozott edzésprogram</returns>
    /// <response code="201">Sikeres létrehozás</response>
    /// <response code="400">Hibás adatok (pl. érvénytelen kódnév)</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin vagy Writer szükséges)</response>
    [Authorize(Roles = "Admin,Writer")]
    public override Task<ActionResult<WorkoutProgramDto>> Create(WorkoutProgramDto dto)
    {
        return base.Create(dto);
    }

    /// <summary>
    /// Meglévő edzésprogram frissítése.
    /// Adminisztrátorok és Writer szerepkörű felhasználók használhatják.
    /// </summary>
    /// <param name="id">A frissítendő edzésprogram azonosítója</param>
    /// <param name="dto">Az új adatok</param>
    /// <returns>A frissített edzésprogram</returns>
    /// <response code="200">Sikeres frissítés</response>
    /// <response code="400">Hibás adatok (pl. érvénytelen kódnév)</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin vagy Writer szükséges)</response>
    /// <response code="404">Az edzésprogram nem található</response>
    [Authorize(Roles = "Admin,Writer")]
    public override Task<ActionResult<WorkoutProgramDto>> Update(int id, WorkoutProgramDto dto)
    {
        return base.Update(id, dto);
    }
}
