using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IGeoLocationRepository _geoLocationRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(ICompanyRepository companyRepository, RepositoryContext context, IAddressRepository addressRepository, IGeoLocationRepository geoLocationRepository)
        {
            _companyRepository = companyRepository;
            _context = context;
            _addressRepository = addressRepository;
            _geoLocationRepository = geoLocationRepository;
        }

        public ICompanyRepository Company => _companyRepository;
        public IAddressRepository Address => _addressRepository;
        public IGeoLocationRepository GeoLocation => _geoLocationRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
