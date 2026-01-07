using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;

namespace WorkoutManager.Controllers;

/// <summary>
/// Az alkalmazás felhasználóinak kezelésére szolgáló controller.
/// Kezeli a felhasználói adatok lekérdezését és a kedvenc edzésprogramok hozzáadását/törlését.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ApplicationUserController(IApplicationUserService applicationUserService, IMapper mapper)
    : ControllerBase
{
    /// <summary>
    /// Lekér egy felhasználót azonosító alapján.
    /// </summary>
    /// <param name="id">A felhasználó egyedi azonosítója</param>
    /// <returns>A felhasználó adatai UserDto formátumban</returns>
    /// <response code="200">A felhasználó sikeresen lekérve</response>
    /// <response code="404">A felhasználó nem található</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(string id)
    {
        var entity = await applicationUserService.GetByIdAsync(id);
        return Ok(mapper.Map<UserDto>(entity));
    }
    
    /// <summary>
    /// Kedvenc edzésprogramok hozzáadása egy felhasználóhoz.
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="userId">A felhasználó azonosítója</param>
    /// <param name="workoutProgramId">Az edzésprogramok azonosítóinak listája</param>
    /// <returns>NoContent válasz, ha sikeres</returns>
    /// <response code="204">A kedvenc edzésprogramok sikeresen hozzáadva</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    /// <response code="404">A felhasználó vagy edzésprogram nem található</response>
    [HttpPost("{userId}/add-workout-program")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddFavouriteWorkoutProgram(
        string userId,
        [FromBody] List<int> workoutProgramId)
    {
        await applicationUserService.AddFavouriteWorkoutProgramAsync(userId, workoutProgramId);
        return NoContent();
    }
    
    /// <summary>
    /// Kedvenc edzésprogramok törlése egy felhasználótól.
    /// Csak adminisztrátorok használhatják.
    /// </summary>
    /// <param name="userId">A felhasználó azonosítója</param>
    /// <param name="workoutProgramIds">A törlendő edzésprogramok azonosítóinak listája</param>
    /// <returns>NoContent válasz, ha sikeres</returns>
    /// <response code="204">A kedvenc edzésprogramok sikeresen törölve</response>
    /// <response code="401">Nincs jogosultság</response>
    /// <response code="403">Nem megfelelő szerepkör (Admin szükséges)</response>
    /// <response code="404">A felhasználó vagy edzésprogram nem található</response>
    [HttpDelete("{userId}/delete-workout-program")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteFavouriteWorkoutProgram(
        string userId,
        [FromBody] List<int> workoutProgramIds)
    {
        await applicationUserService.DeleteFavouriteWorkoutProgramAsync(userId, workoutProgramIds);
        return NoContent();
    }
}