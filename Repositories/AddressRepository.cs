using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Address>> GetAddressByUserIdAsync(int userId)
        {
            var address = await FindByConditionAsync(a => a.ApplicationUserId.Equals(userId),false);
            
            return address;
        }

        public async Task CreateAddressAsync( Address address) => await CreateAsync(address);

        public async Task DeleteAddressAsync(Address address)
        {
            await DeleteAsync(address);
        }

        public async Task UpdateAddressAsync(Address address)
        {
            await UpdateAsync(address);
        }
    }
}
