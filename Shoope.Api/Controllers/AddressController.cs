using Microsoft.AspNetCore.Mvc;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        public readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("v1/public/address/get-address-by-id/{addressId}")]
        public async Task<IActionResult> GetAddressById([FromRoute] string addressId)
        {
            var result = await _addressService.GetAddressById(Guid.Parse(addressId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/address/get-address-by-user-id/{userId}")]
        public async Task<IActionResult> GetAddressByUserId([FromRoute] string userId)
        {
            var result = await _addressService.GetAddressByUserId(Guid.Parse(userId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        

        [HttpPost("v1/public/address/create")]
        public async Task<IActionResult> CreateAsync([FromBody] AddressDTO addressDTO)
        {
            var result = await _addressService.CreateAsync(addressDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
