using AutoMapper;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class EquipmentCategoryProfile : Profile
{
    public EquipmentCategoryProfile()
    {
        CreateMap<EquipmentCategory, EquipmentCategoryDto>().ReverseMap();
    }
}