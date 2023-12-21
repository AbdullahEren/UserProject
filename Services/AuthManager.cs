using AutoMapper;
using Entities.Dtos.UserDto;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly RepositoryContext _context;
        private readonly ILogger<AuthManager> _logger;

        private ApplicationUser? _user;

        public AuthManager(IRepositoryManager manager, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager, RepositoryContext context, ILogger<AuthManager> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<string> CreateToken()
        {
            var signinCredentials = GetSignInCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            //var refreshToken = GenerateRefreshToken();
            //_user.RefreshToken = refreshToken;

            //if (populateExp)
            //    _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            //await _userManager.UpdateAsync(_user);

            //var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            //return new TokenDto()
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken
            //};
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            var company = await _manager.Company.GetCompanyAsync(userDto.CompanyId,false);
            if (company == null)
            {
                throw new CompanyNotFoundException(userDto.CompanyId);
            }
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthDto.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password));
            if (!result)
            {
                _logger.LogWarning($"{nameof(ValidateUser)} : Authentication failed. Wrong username or password.");
            }
            return result;
        }


        private SigningCredentials GetSignInCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager
                .GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,
            List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signinCredentials);

            return tokenOptions;
        }
    }
}
