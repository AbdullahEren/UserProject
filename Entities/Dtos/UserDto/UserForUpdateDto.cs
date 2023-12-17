using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.UserDto
{
    public record UserForUpdateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Username is required.")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password must be between 6 and 20 characters.", MinimumLength = 6)]
        public string? Password { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? Email { get; init; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string? PhoneNumber { get; init; }
        public Address? Address { get; init; }

        [Url(ErrorMessage = "Invalid Website Url.")]
        public string? Website { get; init; }

        [Required(ErrorMessage = "Company Id is required.")]
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; init; }

    }
}
