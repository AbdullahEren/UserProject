using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Catch Phrase is required.")]
        public string CatchPhrase { get; set; }

        [Required(ErrorMessage = "Business Description is required.")]
        public string Bs { get; set; }

        [JsonIgnore]
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
