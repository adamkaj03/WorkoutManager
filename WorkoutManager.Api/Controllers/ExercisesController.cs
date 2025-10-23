using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Data;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController(IExerciseService exerciseService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exercise>>> GetAll()
    {
        var entities = await exerciseService.GetAllExercisesAsync();
        return Ok(entities);
    }
}

