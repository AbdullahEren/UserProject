using Entities.Dtos.AddressDtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace UserProject.Controllers
{
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAddressByUserId(int userId)
        {
            var address = await _addressService.GetAddressByUserIdAsync(userId);
            return Ok(address);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateAddress(int userId, [FromBody] AddressForCreationDto address)
        {
            await _addressService.CreateAddressAsync(userId, address);
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAddress(int userId, [FromBody] AddressForUpdateDto address)
        {
            await _addressService.UpdateAddressAsync(userId, address);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAddress(int userId)
        {
            await _addressService.DeleteAddressAsync(userId);
            return Ok();
        }


    }
}
