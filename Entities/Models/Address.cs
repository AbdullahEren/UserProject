using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Address
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Suite is required.")]
        public string Suite { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        public string ZipCode { get; set; }
        public GeoLocation? Geo { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ApplicationUser))]
        public int ApplicationUserId { get; set; }

        [JsonIgnore]
        
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
