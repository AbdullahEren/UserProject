using Entities.Dtos.AddressDtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.UserDto
{
    public record UserForCacheUpdateDto
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? UserName { get; init; }
        public string? Password { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Website { get; init; }
        public int? CompanyId { get; init; }
        public string? PasswordHash { get; init; }
        public string? SecurityStamp { get; init; }
        public string? ConcurrencyStamp { get; init; }
        public bool? LockoutEnabled { get; init; }
    }
}
