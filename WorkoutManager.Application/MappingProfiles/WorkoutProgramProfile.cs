using AutoMapper;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class WorkoutProgramProfile : Profile
{
    public WorkoutProgramProfile()
    {
        CreateMap<WorkoutProgram, WorkoutProgramDto>().ReverseMap();
    }
}