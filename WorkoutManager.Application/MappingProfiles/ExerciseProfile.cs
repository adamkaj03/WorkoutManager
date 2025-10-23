using AutoMapper;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<Exercise, ExerciseDto>().ReverseMap();
    }
}