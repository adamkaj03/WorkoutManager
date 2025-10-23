using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExerciseGroupsController(IExerciseGroupService exerciseGroupService, IMapper mapper)
    : CrudController<ExerciseGroup, ExerciseGroupDto>(exerciseGroupService, mapper)
{
    
}