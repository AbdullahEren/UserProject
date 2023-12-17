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
        IEnumerable<ApplicationRole> Roles { get; }
        IEnumerable<ApplicationUser> Users { get; }
        Task<IEnumerable<ApplicationUser>> GetOneUser(string userName);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<IdentityResult> UpdateUser(UserForUpdateDto userDto);
        Task<IdentityResult> DeleteOneUser(string userName);

    }
}
