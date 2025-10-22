using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.DTOs;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly WorkoutDbContext _context;

    public ExercisesController(WorkoutDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
    {
        var exercises = await _context.Exercises
            .Include(e => e.Equipment)
            .Include(e => e.ExerciseGroup)
            .Include(e => e.Contraindications)
            .ToListAsync();

        var exerciseDtos = exercises.Select(e => new ExerciseDto
        {
            Id = e.Id,
            Name = e.Name,
            Quantity = e.Quantity,
            Unit = e.Unit,
            Order = e.Order,
            ExerciseGroupId = e.ExerciseGroupId,
            ExerciseGroupName = e.ExerciseGroup.Name,
            EquipmentId = e.EquipmentId,
            EquipmentName = e.Equipment?.Name,
            Contraindications = e.Contraindications.Select(c => new ContraindicationDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        }).ToList();

        return Ok(exerciseDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseDto>> GetById(int id)
    {
        var exercise = await _context.Exercises
            .Include(e => e.Equipment)
            .Include(e => e.ExerciseGroup)
            .Include(e => e.Contraindications)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exercise == null)
            return NotFound();

        var exerciseDto = new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Quantity = exercise.Quantity,
            Unit = exercise.Unit,
            Order = exercise.Order,
            ExerciseGroupId = exercise.ExerciseGroupId,
            ExerciseGroupName = exercise.ExerciseGroup.Name,
            EquipmentId = exercise.EquipmentId,
            EquipmentName = exercise.Equipment?.Name,
            Contraindications = exercise.Contraindications.Select(c => new ContraindicationDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        };

        return Ok(exerciseDto);
    }
}

