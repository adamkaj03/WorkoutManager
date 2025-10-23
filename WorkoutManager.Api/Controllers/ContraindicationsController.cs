using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Data;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContraindicationsController(IContraindicationService contraindicationService, IMapper mapper) 
    : CrudController<Contraindication, ContraindicationDto>(contraindicationService, mapper)
{

}
