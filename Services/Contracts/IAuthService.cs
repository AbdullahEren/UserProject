using Entities.Dtos.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userDto);
        Task<IEnumerable<UserForReadDto>> GetOneUser(string userName);
        Task<IEnumerable<UserForReadDto>> GetAllUsers();
        Task<IdentityResult> UpdateUser(string userName,UserForUpdateDto userDto);
        Task<IdentityResult> DeleteOneUser(string userName);

    }
}
