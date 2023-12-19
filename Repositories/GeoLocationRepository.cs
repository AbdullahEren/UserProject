using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GeoLocationRepository : RepositoryBase<GeoLocation>, IGeoLocationRepository
    {
        public GeoLocationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateGeoLocationAsync(GeoLocation geoLocation)
        {
            await CreateAsync(geoLocation);
        }

        public async Task DeleteGeoLocationAsync(GeoLocation geoLocation)
        {
            await DeleteAsync(geoLocation);
        }

        public async Task<GeoLocation> GetGeoLocationByIdAsync(int addressId, bool trackChanges)
        {
            var geoLocation = await FindByConditionAsync(a => a.AddressId.Equals(addressId), trackChanges);
            return geoLocation.SingleOrDefault();
        }

        public async Task UpdateGeoLocationAsync(GeoLocation geoLocation)
        {
            await UpdateAsync(geoLocation);
        }
    }
}
