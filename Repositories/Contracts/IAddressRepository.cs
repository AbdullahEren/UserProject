using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        Task<IEnumerable<Address>> GetAddressByUserIdAsync(int userId);
        Task CreateAddressAsync( Address address);
        Task DeleteAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);

    }
}
