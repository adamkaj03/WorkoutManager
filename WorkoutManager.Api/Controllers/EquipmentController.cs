using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController(IEquipmentService equipmentService, IMapper mapper) 
    : CrudController<Equipment, EquipmentDto>(equipmentService, mapper)
{
    // 6. feladathoz tartozó GET végpont
    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetByCategory(int categoryId)
    {
        var equipments = await equipmentService.GetEquipmentByCategoryAsync(categoryId);
        var dtos = mapper.Map<IEnumerable<EquipmentDto>>(equipments);
        return Ok(dtos);
    }
    
    // 6. feladathoz tartozó GET végpont
    [HttpGet("contraindication/{contraindicationId}")]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetByContraindication(int contraindicationId)
    {
        var equipments = await equipmentService.GetEquipmentByContraindicationAsync(contraindicationId);
        var dtos = mapper.Map<IEnumerable<EquipmentDto>>(equipments);
        return Ok(dtos);
    }
}

