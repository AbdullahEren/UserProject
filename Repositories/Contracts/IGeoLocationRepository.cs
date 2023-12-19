using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IGeoLocationRepository
    {
        Task<GeoLocation> GetGeoLocationByIdAsync(int addressId, bool trackChanges);
        Task CreateGeoLocationAsync(GeoLocation geoLocation);
        Task DeleteGeoLocationAsync(GeoLocation geoLocation);
        Task UpdateGeoLocationAsync(GeoLocation geoLocation);
    }
}
