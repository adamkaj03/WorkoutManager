using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

/// <summary>
/// Eszközkategóriák kezelésére szolgáló controller.
/// Kezeli az eszközkategóriák CRUD műveleteit.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EquipmentCategoriesController(IEquipmentCategoryService equipmentCategoryService, IMapper mapper)
    : CrudController<EquipmentCategory, EquipmentCategoryDto>(equipmentCategoryService, mapper)
{
    
}
