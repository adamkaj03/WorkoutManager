using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutProgramsController(IWorkoutProgramService workoutProgramService, IMapper mapper)
    : CrudController<WorkoutProgram, WorkoutProgramDto>(workoutProgramService, mapper)
{
    // 5. feladathoz tartozó GET végpont
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutProgramDto>> GetFull(int id)
    {
        var entity = await workoutProgramService.GetFullWorkoutProgramAsync(id);
        if (entity == null)
            return NotFound();

        var dto = mapper.Map<WorkoutProgramDto>(entity);
        return Ok(dto);
    }
}

