using Entities.Dtos.AddressDtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressForReadDto>> GetAddressByUserIdAsync(int userId);
        Task CreateAddressAsync(int userId,AddressForCreationDto address);
        Task UpdateAddressAsync(int userId,AddressForUpdateDto address);
            
        Task DeleteAddressAsync(int userId);
    }
}
