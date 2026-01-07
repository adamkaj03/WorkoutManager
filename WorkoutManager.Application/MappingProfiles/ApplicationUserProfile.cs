using AutoMapper;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}