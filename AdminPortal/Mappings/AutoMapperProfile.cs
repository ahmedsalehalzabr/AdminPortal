using AdminPortal.Models.DTOs;
using AdminPortal.Models.Entities;
using AutoMapper;

namespace AdminPortal.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<ElRegionDto, Region> ().ReverseMap();
            CreateMap<ElWalkDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }

}
