using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class UserSellerProductController : ControllerBase
    {
        public readonly IUserSellerProductService _userSellerService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public UserSellerProductController(IUserSellerProductService userSellerService, IBaseController baseController, ICurrentUser currentUser)
        {
            _userSellerService = userSellerService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/get-user-seller-product-by-id/{userSellerProductId}")]
        public async Task<IActionResult> GetById([FromRoute] string userSellerProductId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userSellerService.GetById(Guid.Parse(userSellerProductId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/user-seller-product/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserSellerProductDTO userSellerProductDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userSellerService.Create(userSellerProductDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
