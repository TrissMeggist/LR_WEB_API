using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace LR_WEB_API.Controllers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress,opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Ship, ShipDTO>();
            CreateMap<Port, PortDTO>();
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
