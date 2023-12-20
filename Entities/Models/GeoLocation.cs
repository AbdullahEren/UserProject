using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GeoLocation
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeoLocationId { get; set; }
        [Required(ErrorMessage = "Latitude is required.")]
        public decimal Lat { get; set; }

        [Required(ErrorMessage = "Longtitude is required.")]
        public decimal Lng { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(Address))]
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }
}
