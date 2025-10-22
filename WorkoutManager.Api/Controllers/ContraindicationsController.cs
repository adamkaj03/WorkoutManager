using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.DTOs;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContraindicationsController : ControllerBase
{
    private readonly WorkoutDbContext _context;

    public ContraindicationsController(WorkoutDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContraindicationDetailDto>>> GetAll()
    {
        var contraindications = await _context.Contraindications
            .Include(c => c.Exercises)
            .Include(c => c.Equipment)
            .ToListAsync();

        var contraindicationDtos = contraindications.Select(c => new ContraindicationDetailDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            RelatedExercises = c.Exercises.Select(e => e.Name).ToList(),
            RelatedEquipment = c.Equipment.Select(e => e.Name).ToList()
        }).ToList();

        return Ok(contraindicationDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContraindicationDetailDto>> GetById(int id)
    {
        var contraindication = await _context.Contraindications
            .Include(c => c.Exercises)
            .Include(c => c.Equipment)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contraindication == null)
            return NotFound();

        var contraindicationDto = new ContraindicationDetailDto
        {
            Id = contraindication.Id,
            Name = contraindication.Name,
            Description = contraindication.Description,
            RelatedExercises = contraindication.Exercises.Select(e => e.Name).ToList(),
            RelatedEquipment = contraindication.Equipment.Select(e => e.Name).ToList()
        };

        return Ok(contraindicationDto);
    }
}
