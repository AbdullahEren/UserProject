using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Dtos.GeoLocationDtos
{
    public record GeoLocationDto
    {
        
        [Required(ErrorMessage = "Latitude is required.")]
        public decimal Lat { get; init; }

        [Required(ErrorMessage = "Longitude is required.")]
        public decimal Lng { get; init; }

        [JsonIgnore]
        public int? AddressId { get; init; }
    }
}
