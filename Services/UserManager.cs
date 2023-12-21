using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Repositories;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos.UserDto;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RepositoryContext _context;
        private readonly ILogger<AuthManager> _logger;

        private ApplicationUser? _user;

        public UserManager(IRepositoryManager manager, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager, RepositoryContext context, ILogger<AuthManager> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<UserForReadDto>> GetAllUsers()
        {
            var users = await _userManager.Users
                .Include(u => u.Company)
                .Include(u => u.Address)
                .ThenInclude(a => a.Geo)
                .Where(u => u.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserForReadDto>>(users);
        }

        public async Task<IEnumerable<UserForReadDto>> GetOneUser(string userName)
        {
            var user = await _userManager.Users
                .Include(u => u.Company)
                .Include(u => u.Address)
                .ThenInclude(a => a.Geo)
                .Where(u => u.UserName == userName && u.IsDeleted == false)
                .ToListAsync();

            if (user == null)
            {
                throw new UserNotFoundException(userName);
            }

            var userDto = _mapper.Map<IEnumerable<UserForReadDto>>(user);

            return userDto;
        }

        public async Task<IdentityResult> UpdateUser(string userName, UserForUpdateDto userDto)
        {
            var company = await _manager.Company.GetCompanyAsync(userDto.CompanyId, false);
            if (company == null)
            {
                throw new CompanyNotFoundException(userDto.CompanyId);
            }

            var userEntity = await _context.Users.Where(u => u.UserName == userName && u.IsDeleted == false).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                throw new UserNotFoundException(userName);
            }

            _mapper.Map(userDto, userEntity);
            var result = await _userManager.UpdateAsync(userEntity);

            return result;
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await _context.Users.Where(u => u.UserName == userName && u.IsDeleted == false).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserNotFoundException(userName);
            }
            user.IsDeleted = true;
            var address = await _context.Addresses.Where(u => u.ApplicationUserId.Equals(user.Id)).FirstOrDefaultAsync();

            if (address != null)
            {

                address.IsDeleted = true;
                var geo = await _context.GeoLocations.Where(u => u.AddressId.Equals(address.AddressId)).FirstOrDefaultAsync();
                if (geo != null)
                {
                    geo.IsDeleted = true;
                }
            }
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}
