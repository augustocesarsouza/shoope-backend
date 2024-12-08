using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;
using Shoope.Domain.Entities;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        public readonly IProductDetailService _productDetailService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductDetailController(IProductDetailService productDetailService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productDetailService = productDetailService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/product-detail/get-product-detail-by-product-id/{productId}")]
        public async Task<IActionResult> GetFlashSaleProductByProductFlashSaleId([FromRoute] string productId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _productDetailService.GetProductDetailByProductId(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/product-detail/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDetailDTO productDetailDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productDetailService.CreateAsync(productDetailDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
