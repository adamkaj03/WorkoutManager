using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.DTOs;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentCategoriesController : ControllerBase
{
    private readonly WorkoutDbContext _context;

    public EquipmentCategoriesController(WorkoutDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentCategoryDto>>> GetAll()
    {
        var categories = await _context.EquipmentCategories
            .Include(c => c.Equipment)
            .ToListAsync();

        var categoryDtos = categories.Select(c => new EquipmentCategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Equipment = c.Equipment.Select(e => new EquipmentSimpleDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList()
        }).ToList();

        return Ok(categoryDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentCategoryDto>> GetById(int id)
    {
        var category = await _context.EquipmentCategories
            .Include(c => c.Equipment)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return NotFound();

        var categoryDto = new EquipmentCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Equipment = category.Equipment.Select(e => new EquipmentSimpleDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList()
        };

        return Ok(categoryDto);
    }
}

