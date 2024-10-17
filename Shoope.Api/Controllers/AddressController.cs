using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        public readonly IAddressService _addressService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public AddressController(IAddressService addressService, IBaseController baseController,
             ICurrentUser currentUser)
        {
            _addressService = addressService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/public/address/get-address-by-id/{addressId}")]
        public async Task<IActionResult> GetAddressById([FromRoute] string addressId)
        {
            var result = await _addressService.GetAddressById(Guid.Parse(addressId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/address/get-address-by-user-id/{userId}")]
        public async Task<IActionResult> GetAddressByUserId([FromRoute] string userId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.GetAddressByUserId(Guid.Parse(userId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPost("v1/address/create")]
        public async Task<IActionResult> CreateAsync([FromBody] AddressDTO addressDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.CreateAsync(addressDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("v1/address/update")]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDTO addressDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.UpdateAddressUser(addressDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("v1/address/update-only-default-address")]
        public async Task<IActionResult> UpdateAsyncOnlyDefaultAddress([FromBody] AddressDTO addressDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.UpdateAsyncOnlyDefaultAddress(addressDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpDelete("v1/address/delete/{addressId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string addressId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.Delete(Guid.Parse(addressId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
