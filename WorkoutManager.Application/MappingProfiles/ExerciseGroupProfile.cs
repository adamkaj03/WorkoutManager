using AutoMapper;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class ExerciseGroupProfile : Profile
{
    public ExerciseGroupProfile()
    {
        CreateMap<ExerciseGroup, ExerciseGroupDto>().ReverseMap();
    }
}