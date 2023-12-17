using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public Address? Address { get; set; }

        [Url(ErrorMessage = "Invalid Website Url.")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Company Id is required.")]
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        
        public Company? Company { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

    }
}
