using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Data;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContraindicationsController(IContraindicationService contraindicationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contraindication>>> GetAll()
    {
        var entities = await contraindicationService.GetAllContraindicationsAsync();
        return Ok(entities);
    }
}
