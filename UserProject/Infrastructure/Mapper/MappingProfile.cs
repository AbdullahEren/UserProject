using AutoMapper;
using Entities.Dtos.CompanyDto;
using Entities.Dtos.UserDto;
using Entities.Models;

namespace UserProject.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company,CompanyForCreationDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<Company, CompanyForUpdateDto>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<ApplicationUser, UserForRegistrationDto>();
            CreateMap<UserForRegistrationDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserForUpdateDto>();
            CreateMap<UserForUpdateDto, ApplicationUser>();
        }
    }
}
