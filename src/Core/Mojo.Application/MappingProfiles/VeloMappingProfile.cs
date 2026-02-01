using AutoMapper;
using Mojo.Domain.Entities;
using Mojo.Application.DTOs.EntitiesDto.Velo;

namespace Mojo.Application.MappingProfiles
{
    public class VeloMappingProfile : Profile
    {
        public VeloMappingProfile()
        {
            CreateMap<Velo, VeloDto>().ReverseMap();
        }
    }
}