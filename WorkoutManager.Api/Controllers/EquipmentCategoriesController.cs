using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Data;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentCategoriesController(IEquipmentCategoryService equipmentCategoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentCategory>>> GetAll()
    {
        var entites = await equipmentCategoryService.GetAllEquipmentCategoriesAsync();
        return Ok(entites);
    }
}

