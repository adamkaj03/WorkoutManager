using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.DTOs;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly WorkoutDbContext _context;

    public EquipmentController(WorkoutDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAll()
    {
        var equipment = await _context.Equipment
            .Include(e => e.EquipmentCategory)
            .Include(e => e.Contraindications)
            .ToListAsync();

        var equipmentDtos = equipment.Select(e => new EquipmentDto
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            EquipmentCategoryId = e.EquipmentCategoryId,
            EquipmentCategoryName = e.EquipmentCategory.Name,
            Contraindications = e.Contraindications.Select(c => new ContraindicationDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        }).ToList();

        return Ok(equipmentDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentDto>> GetById(int id)
    {
        var equipment = await _context.Equipment
            .Include(e => e.EquipmentCategory)
            .Include(e => e.Contraindications)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (equipment == null)
            return NotFound();

        var equipmentDto = new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Description = equipment.Description,
            EquipmentCategoryId = equipment.EquipmentCategoryId,
            EquipmentCategoryName = equipment.EquipmentCategory.Name,
            Contraindications = equipment.Contraindications.Select(c => new ContraindicationDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        };

        return Ok(equipmentDto);
    }
}

