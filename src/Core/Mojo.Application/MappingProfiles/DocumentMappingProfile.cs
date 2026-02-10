using AutoMapper;
using Mojo.Application.DTOs.EntitiesDto.Documents;
using Mojo.Domain.Entities;

namespace Mojo.Application.MappingProfiles
{
    public class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<Documents, DocumentDto>().ReverseMap();
        }
    }
}