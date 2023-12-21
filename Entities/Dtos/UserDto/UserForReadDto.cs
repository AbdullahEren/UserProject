using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos.AddressDtos;

namespace Entities.Dtos.UserDto
{
    public record UserForReadDto
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public AddressForReadDto? Address { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Website { get; init; }
        public Company? Company { get; init; }
    }
}
