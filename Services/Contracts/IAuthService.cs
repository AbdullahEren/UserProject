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
        Task<string> Login(string username, string password);
        Task<string> Register(string username, string password);
        IEnumerable<ApplicationRole> Roles { get; }
        IEnumerable<ApplicationUser> Users { get; }
        Task<IEnumerable<ApplicationUser>> GetOneUser(string userName);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<IdentityResult> CreateUser(ApplicationUser userDto);
        Task<IdentityResult> DeleteOneUser(string userName);

    }
}
