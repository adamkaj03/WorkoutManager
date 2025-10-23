using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExerciseGroupsController(IExerciseGroupService exerciseGroupService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseGroup>>> GetAll()
    {
        var entities = await exerciseGroupService.GetAllExerciseGroupsAsync();
        return Ok(entities);
    }
}