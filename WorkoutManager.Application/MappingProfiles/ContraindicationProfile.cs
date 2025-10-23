using AutoMapper;
using WorkoutManager.DTOs;
using WorkoutManager.Models;

namespace WorkoutManager.Application.MappingProfiles;

public class ContraindicationProfile : Profile
{
    public ContraindicationProfile()
    {
        CreateMap<Contraindication, ContraindicationDto>().ReverseMap();
    }
}