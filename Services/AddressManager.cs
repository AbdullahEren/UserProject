using AutoMapper;
using Entities.Dtos.AddressDtos;
using Entities.Dtos.UserDto;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class AddressManager : IAddressService
    {
        private readonly IRepositoryManager _manager;
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;

        public AddressManager(IRepositoryManager manager, IMapper mapper, RepositoryContext context, ICacheService cache)
        {
            _manager = manager;
            _mapper = mapper;
            _context = context;
            _cache = cache;
        }

        public async Task CreateAddressAsync(int userId, AddressForCreationDto addressDto)
        {
            if (addressDto == null)
            {
                throw new AddressNotFoundException();
            }

            var user = await _context.Users
            .Where(u => u.Id == userId && u.IsDeleted == false)
            .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var addressEntity = await _context.Addresses.Where(u => u.ApplicationUserId == userId && u.IsDeleted == false).FirstOrDefaultAsync();
            if (addressEntity != null)
            {
                throw new UserHasAddressException();
            }
           
            var address = _mapper.Map<Address>(addressDto);
            address.ApplicationUserId = userId;
            var geo = _mapper.Map<GeoLocation>(addressDto.Geo);
            geo.AddressId = address.AddressId;

            await _manager.Address.CreateAddressAsync(address);
            await _manager.SaveAsync();
            await _cache.RemoveAsync($"User:{user.UserName}");
        }

        public async Task UpdateAddressAsync(int userId, AddressForUpdateDto addressDto)
        {
            if (addressDto == null)
            {
                throw new AddressNotFoundException();
            }
            
            var user = await _context.Users
            .Where(u => u.Id == userId && u.IsDeleted == false)
            .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var addressEntity = await _cache.GetAsync($"Address:{userId}", async () =>
            {
                var address = await _context.Addresses
                    .Include(u => u.Geo)
                    .Where(u => u.ApplicationUserId == userId && u.IsDeleted == false)
                    .FirstOrDefaultAsync();

                if (address == null)
                {
                    throw new UserHasNoAddressException();
                }
                
                return address;
            });

            var address = _mapper.Map(addressDto, addressEntity);
            _mapper.Map(addressDto.Geo, addressEntity.Geo);
            address.ApplicationUserId = userId;
            
            address.AddressId = addressEntity.AddressId;
            address.Geo.AddressId = addressEntity.AddressId;
            address.Geo.GeoLocationId = addressEntity.Geo.GeoLocationId;


            await _cache.RemoveAsync($"Address:{userId}");
            await _manager.SaveAsync();
            await _cache.RemoveAsync($"User:{user.UserName}");

        }

        public async Task DeleteAddressAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId && u.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var addressEntity = await _cache.GetAsync($"Address:{userId}", async () =>
            {
                var address = await _context.Addresses
                    .Include(u => u.Geo)
                    .Where(u => u.ApplicationUserId == userId && u.IsDeleted == false)
                    .FirstOrDefaultAsync();

                if (address == null)
                {
                    throw new UserHasNoAddressException();
                }

                return address;
            });

            await _manager.GeoLocation.DeleteGeoLocationAsync(addressEntity.Geo);
            await _manager.Address.DeleteAddressAsync(addressEntity);
            await _manager.SaveAsync();

            await _cache.RemoveAsync($"Address:{userId}");
            await _cache.RemoveAsync($"User:{user.UserName}");
        }

        public async Task<IEnumerable<AddressForReadDto>> GetAddressByUserIdAsync(int userId)
        {
            return await _cache.GetAsync($"Addresses:{userId}", async () =>
            {
                var address = await _context.Addresses
                    .Include(u => u.Geo)
                    .Where(u => u.ApplicationUserId == userId && u.IsDeleted == false)
                    .ToListAsync();

                if (address == null)
                {
                    throw new AddressNotFoundException(userId);
                }
            var addressDtos = _mapper.Map<IEnumerable<AddressForReadDto>>(address);

            return addressDtos.ToList();
            });

        }

    }
}
