using AutoMapper;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class EquipmentProfile : Profile
{
    public EquipmentProfile()
    {
        CreateMap<Equipment, EquipmentDto>().ReverseMap();
    }
}