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
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();

            CreateMap<Ship, ShipDTO>();
            CreateMap<ShipForCreationDto, Ship>();
            CreateMap<ShipForUpdateDto, Ship>().ReverseMap();

            CreateMap<Port, PortDTO>();
            CreateMap<PortForCreationDto, Port>();
            CreateMap<PortForUpdateDto, Port>();

            CreateMap<UserForRegistrationDto, User>();

        }
    }
}
