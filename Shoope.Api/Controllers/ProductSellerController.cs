using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class ProductSellerController : ControllerBase
    {
        public readonly IProductSellerService _productSellerService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductSellerController(IProductSellerService productSellerService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productSellerService = productSellerService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/product-seller/get-user-seller-product-id/{productId}")]
        public async Task<IActionResult> GetById([FromRoute] string productId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _productSellerService.GetById(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPost("v1/product-seller/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductSellerDTO productSellerDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productSellerService.Create(productSellerDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
