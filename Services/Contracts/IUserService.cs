using Entities.Dtos.UserDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserForReadDto>> GetOneUser(string userName);
        Task<IEnumerable<UserForReadDto>> GetAllUsers();
        Task<IdentityResult> UpdateUser(string userName, UserForUpdateDto userDto);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}
