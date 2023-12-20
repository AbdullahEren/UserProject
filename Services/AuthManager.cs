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
            user.IsDeleted = true;
            if (user.Address != null)
            {
                user.Address.IsDeleted = true;

                if (user.Address.Geo != null)
                {
                    user.Address.Geo.IsDeleted = true;
                }
            }
            var result = await _userManager.UpdateAsync(user);
            return result;
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
                throw new ArgumentNullException(nameof(user));
            }

            var userDto = _mapper.Map<IEnumerable<UserForReadDto>>(user);

            return userDto;
        }


        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            var company = await _manager.Company.GetCompanyAsync(userDto.CompanyId,false);
            if (company == null)
            {
                throw new Exception("Company can not found");
            }
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
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

            _mapper.Map(userDto, userEntity);
            var result = await _userManager.UpdateAsync(userEntity);

            return result;
        }


    }
}
