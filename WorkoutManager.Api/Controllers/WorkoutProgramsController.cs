using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
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
    [HttpGet("full/{id}")]
    public async Task<ActionResult<WorkoutProgramDto>> GetFull(int id)
    {
        var entity = await workoutProgramService.GetFullWorkoutProgramAsync(id);
        if (entity == null)
            return NotFound();

        var dto = mapper.Map<WorkoutProgramDto>(entity);
        return Ok(dto);
    }
    
    [HttpPost("{workoutProgramId}/exercise-groups")]
    public async Task<IActionResult> AssignExerciseGroupsToWorkoutPrograms(
        int workoutProgramId,
        [FromBody] List<int> exerciseGroupIds)
    {
        await workoutProgramService.AssignExerciseGroupsAsync(workoutProgramId, exerciseGroupIds);
        return NoContent();
    }
    
    [HttpGet("by-exercise/{exerciseId}/contraindications")]
    public async Task<ActionResult<IEnumerable<WorkoutProgramWithContraindicationsDto>>> GetAllTitleAndContraindicationByExerciseAsync(
        int exerciseId)
    {
        var dtos = await workoutProgramService.GetAllTitleAndContraindicationByExerciseAsync(exerciseId);
        return Ok(dtos);
    }
}

