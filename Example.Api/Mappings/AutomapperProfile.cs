using AutoMapper;
using Example.Domain.Entities;
using Example.Domain.Service;

namespace Example.Api.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<TypePermit, TypePermitDto>().ReverseMap();
        }
    }
    
}
