using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.DTOs;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutProgramsController : ControllerBase
{
    private readonly WorkoutDbContext _context;

    public WorkoutProgramsController(WorkoutDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutProgramDto>>> GetAll()
    {
        var programs = await _context.WorkoutPrograms
            .Include(p => p.ExerciseGroups)
                .ThenInclude(eg => eg.Exercises)
                    .ThenInclude(e => e.Equipment)
            .ToListAsync();

        var programDtos = programs.Select(p => new WorkoutProgramDto
        {
            Id = p.Id,
            CodeName = p.CodeName,
            Title = p.Title,
            Description = p.Description,
            ExerciseGroups = p.ExerciseGroups.Select(eg => new ExerciseGroupDto
            {
                Id = eg.Id,
                Name = eg.Name,
                Order = eg.Order,
                WorkoutProgramId = eg.WorkoutProgramId,
                Exercises = eg.Exercises.Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Quantity = e.Quantity,
                    Unit = e.Unit,
                    Order = e.Order,
                    ExerciseGroupId = e.ExerciseGroupId,
                    ExerciseGroupName = eg.Name,
                    EquipmentId = e.EquipmentId,
                    EquipmentName = e.Equipment?.Name
                }).ToList()
            }).ToList()
        }).ToList();

        return Ok(programDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutProgramDto>> GetById(int id)
    {
        var program = await _context.WorkoutPrograms
            .Include(p => p.ExerciseGroups)
                .ThenInclude(eg => eg.Exercises)
                    .ThenInclude(e => e.Equipment)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (program == null)
            return NotFound();

        var programDto = new WorkoutProgramDto
        {
            Id = program.Id,
            CodeName = program.CodeName,
            Title = program.Title,
            Description = program.Description,
            ExerciseGroups = program.ExerciseGroups.Select(eg => new ExerciseGroupDto
            {
                Id = eg.Id,
                Name = eg.Name,
                Order = eg.Order,
                WorkoutProgramId = eg.WorkoutProgramId,
                Exercises = eg.Exercises.Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Quantity = e.Quantity,
                    Unit = e.Unit,
                    Order = e.Order,
                    ExerciseGroupId = e.ExerciseGroupId,
                    ExerciseGroupName = eg.Name,
                    EquipmentId = e.EquipmentId,
                    EquipmentName = e.Equipment?.Name
                }).ToList()
            }).ToList()
        };

        return Ok(programDto);
    }
}

