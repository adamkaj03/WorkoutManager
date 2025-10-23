using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Data;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController(IEquipmentService equipmentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetAll()
    {
        var entities = await equipmentService.GetAllEquipmentAsync();
        return Ok(entities);
    }
}

