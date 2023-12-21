using AutoMapper;
using Entities.Dtos.AddressDtos;
using Entities.Dtos.UserDto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AddressManager : IAddressService
    {
        private readonly IRepositoryManager _manager;
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public AddressManager(IRepositoryManager manager, IMapper mapper, RepositoryContext context)
        {
            _manager = manager;
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateAddressAsync(int userId, AddressForCreationDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto));
            }

            var user = await _context.Users.Where(u => u.Id == userId && u.IsDeleted == false).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("The User is null");
            }

            var addressEntity = await _context.Addresses.Where(u => u.ApplicationUserId == userId && u.IsDeleted == false).FirstOrDefaultAsync();
            if (addressEntity != null)
            {
                throw new Exception("The User already has an address");
            }

            var address = _mapper.Map<Address>(addressDto);
            address.ApplicationUserId = userId;
            var geo = _mapper.Map<GeoLocation>(addressDto.Geo);
            geo.AddressId = address.AddressId;

            await _manager.Address.CreateAddressAsync(address);
            await _manager.SaveAsync();
        }


        public async Task UpdateAddressAsync(int userId,AddressForUpdateDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto));
            }
            var user = await _context.Users.Where(u => u.Id == userId && u.IsDeleted == false).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var addressEntity = await _context.Addresses.Include(u => u.Geo).Where(u => u.ApplicationUserId == userId && u.IsDeleted == false).FirstOrDefaultAsync();
            if (addressEntity == null)
            {
                throw new Exception("The User don't have an address");
            }

            var address = _mapper.Map<Address>(addressDto);
            address.ApplicationUserId = userId;
            var geo = _mapper.Map<GeoLocation>(addressDto.Geo);
            geo.AddressId = address.AddressId;
            await _manager.Address.UpdateAddressAsync(address);

            await _manager.SaveAsync();
        }

        public async Task DeleteAddressAsync(int userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId && u.IsDeleted == false).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var addressEntity = await _context.Addresses.Include(u => u.Geo).Where(u => u.ApplicationUserId == userId && u.IsDeleted == false).FirstOrDefaultAsync();
            if (addressEntity == null)
            {
                throw new Exception("The User don't have an address");
            }
            await _manager.GeoLocation.DeleteGeoLocationAsync(addressEntity.Geo);
            await _manager.Address.DeleteAddressAsync(addressEntity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<AddressForReadDto>> GetAddressByUserIdAsync(int userId)
        {
            var address = await _context.Addresses.Include(u => u.Geo).Where(u => u.ApplicationUserId == userId && u.IsDeleted == false).ToListAsync();

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }
            var addressDto = _mapper.Map<IEnumerable<AddressForReadDto>>(address);
            return addressDto;
        }

    }
}
