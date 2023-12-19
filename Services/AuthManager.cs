using AutoMapper;
using Entities.Dtos.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationUser? _user;

        public AuthManager(IRepositoryManager manager, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var address = await _manager.Address.GetAddressByUserIdAsync(user.Id);
            var addressDelete = _mapper.Map<Address>(address);
            if (address != null)
            {
                await _manager.Address.DeleteAddressAsync(addressDelete);
            }

            return await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.Include(u => u.Address).Include(u => u.Address.Geo).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetOneUser(string userName)
        {
            var user = await _userManager.Users.Include(u => u.Address).Where(u => u.UserName == userName).ToListAsync();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            var company = await _manager.Company.GetCompanyAsync(userDto.CompanyId,false);
            if (company == null)
            {
                throw new Exception("Company can not found");
            }

            if (result.Succeeded)
            {
                if (userDto.Address != null)
                {
                    var address = _mapper.Map<Address>(userDto.Address);
                    var geoLocation = _mapper.Map<GeoLocation>(userDto.Address.Geo);

                    address.ApplicationUserId = user.Id;
                    geoLocation.AddressId = address.AddressId;
                    geoLocation.Address = address;

                    // Adresi ve GeoLocation'ı ekleyelim
                    await _manager.Address.CreateAddressAsync(address);
                    await _manager.GeoLocation.CreateGeoLocationAsync(geoLocation);
                }
                else
                {
                    throw new Exception("Address can not be null");
                }
            }

            return result;
        }



        public async Task<IdentityResult> UpdateUser(string userName, UserForUpdateDto userDto)
        {
            var company = await _manager.Company.GetCompanyAsync(userDto.CompanyId, false);
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            var userEntity = await _userManager.FindByNameAsync(userName);
            if (userEntity == null)
            {
                throw new ArgumentNullException(nameof(userEntity));
            }

            // Kullanıcı bilgilerini güncelle
            _mapper.Map(userDto, userEntity);
            var result = await _userManager.UpdateAsync(userEntity);

            if (result.Succeeded)
            {
                // Adres güncelleme
                var address = await _manager.Address.GetAddressByUserIdAsync(userEntity.Id);
                if (address == null)
                {
                    // Adres bulunamadıysa yeni bir adres oluştur
                    if (userDto.Address == null)
                    {
                        throw new Exception("Address can not be null.");
                    }

                    address = _mapper.Map<Address>(userDto.Address);
                    address.ApplicationUserId = userEntity.Id;
                    await _manager.Address.CreateAddressAsync(address);
                }
                else
                {
                    // Adresi güncelle
                    _mapper.Map(userDto.Address, address);
                    await _manager.Address.UpdateAddressAsync(address);
                }

                // GeoLocation güncelleme
                var geoLocation = await _manager.GeoLocation.GetGeoLocationByIdAsync(userEntity.Id, false);
                if (geoLocation != null)
                {
                    // GeoLocation bulunduysa güncelle
                    _mapper.Map(userDto.Address.Geo, geoLocation);
                    await _manager.GeoLocation.UpdateGeoLocationAsync(geoLocation);
                }
                else
                {
                    // GeoLocation bulunamadıysa yeni bir GeoLocation oluştur
                    geoLocation = _mapper.Map<GeoLocation>(userDto.Address.Geo);
                    geoLocation.AddressId = address.AddressId;
                    await _manager.GeoLocation.CreateGeoLocationAsync(geoLocation);
                }
            }

            return result;
        }


    }
}
