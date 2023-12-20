using Entities.Dtos.GeoLocationDtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Dtos.AddressDtos
{
    public record AddressForCreationDto
    {

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; init; }

        [Required(ErrorMessage = "Suite is required.")]
        public string Suite { get; init; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; init; }

        [Required(ErrorMessage = "Zip Code is required.")]
        public string ZipCode { get; init; }

        [JsonIgnore]
        public int? ApplicationUserId { get; init; }
        public virtual GeoLocationDto? Geo { get; set;}
    }
}
