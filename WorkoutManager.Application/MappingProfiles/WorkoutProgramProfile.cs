using AutoMapper;
using WorkoutManager.Application.DTOs;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class WorkoutProgramProfile : Profile
{
    public WorkoutProgramProfile()
    {
        CreateMap<WorkoutProgram, WorkoutProgramDto>()
            .ForMember(
                dest => dest.WarmupDurationMinutes,
                opt => opt.MapFrom(src => src.WarmupDurationMinutes ?? 0)
            )
            .ReverseMap();
    }
}