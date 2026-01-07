using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

/// <summary>
/// Gyakorlatcsoportok kezelésére szolgáló controller.
/// Kezeli a gyakorlatcsoportok CRUD műveleteit.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExerciseGroupsController(IExerciseGroupService exerciseGroupService, IMapper mapper)
    : CrudController<ExerciseGroup, ExerciseGroupDto>(exerciseGroupService, mapper)
{
    
}