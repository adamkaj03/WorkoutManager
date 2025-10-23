using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutProgramsController(IWorkoutProgramService workoutProgramService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutProgram>>> GetAll()
    {
        var entities = await workoutProgramService.GetAllWorkoutProgramsAsync();
        return Ok(entities);
    }
}

