using AutoMapper;
using Entities.Dtos.AddressDtos;
using Entities.Dtos.CompanyDto;
using Entities.Dtos.GeoLocationDtos;
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
            CreateMap<UserForReadDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserForReadDto>();
            CreateMap<GeoLocation, GeoLocationDto>();
            CreateMap<GeoLocationDto, GeoLocation>();
            CreateMap<AddressForCreationDto, Address>();
            CreateMap<Address, AddressForCreationDto>();
            CreateMap<Address, AddressForUpdateDto>();
            CreateMap<AddressForUpdateDto, Address>();
            CreateMap<Address, AddressForReadDto>();
            CreateMap<AddressForReadDto, Address>();
            CreateMap<UserForCacheReadDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserForCacheReadDto>();
            CreateMap<UserForCacheReadDto, UserForReadDto>();
            CreateMap<UserForReadDto, UserForCacheReadDto>();
            CreateMap<UserForCacheReadDto, UserForCacheUpdateDto>();
            CreateMap<UserForCacheUpdateDto, UserForCacheReadDto>();
            CreateMap<UserForCacheUpdateDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserForCacheUpdateDto>();
            CreateMap<UserForUpdateDto, UserForCacheUpdateDto>();
            CreateMap<UserForCacheUpdateDto, UserForUpdateDto>();


        }
    }
}
