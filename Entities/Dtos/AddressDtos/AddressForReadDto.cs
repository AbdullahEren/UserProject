using Entities.Dtos.GeoLocationDtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Dtos.AddressDtos
{
    public record AddressForReadDto
    {
        public int AddressId { get; init; }
        public string Street { get; init; }
        public string Suite { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }
        public int? ApplicationUserId { get; init; }
        public GeoLocationDto? Geo { get; init; }
    }
}
