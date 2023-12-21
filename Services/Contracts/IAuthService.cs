using Entities.Dtos.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userDto);
        Task<bool> ValidateUser(UserForAuthenticationDto userDto);

        Task<string> CreateToken();



    }
}
