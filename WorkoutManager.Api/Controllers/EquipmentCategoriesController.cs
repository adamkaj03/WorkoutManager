using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentCategoriesController(IEquipmentCategoryService equipmentCategoryService, IMapper mapper)
    : CrudController<EquipmentCategory, EquipmentCategoryDto>(equipmentCategoryService, mapper)
{
    
}

