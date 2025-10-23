using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController(IExerciseService exerciseService, IMapper mapper) 
    : CrudController<Exercise, ExerciseDto>(exerciseService, mapper)
{
    // 6. feladathoz tartozó GET végpont
    [HttpGet("contraindication/{contraindicationId}")]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetByContraindication(int contraindicationId)
    {
        var entities = await exerciseService.GetExerciseByContraindicationAsync(contraindicationId);
        var dtos = mapper.Map<IEnumerable<ExerciseDto>>(entities);
        return Ok(dtos);
    }
}

