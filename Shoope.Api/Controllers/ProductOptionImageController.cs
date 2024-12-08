using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class ProductOptionImageController : ControllerBase
    {
        public readonly IProductOptionImageService _productOptionImageService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductOptionImageController(IProductOptionImageService productOptionImageService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productOptionImageService = productOptionImageService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/product-option-image/get-by-list-flash-sale-product-image-all-id/{productsOfferFlashId}")]
        public async Task<IActionResult> GetByListFlashSaleProductImageAllId([FromRoute] string productsOfferFlashId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _productOptionImageService.GetByListFlashSaleProductImageAllId(Guid.Parse(productsOfferFlashId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPost("v1/product-option-image/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductOptionImageDTO productOptionImageDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productOptionImageService.Create(productOptionImageDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpDelete("v1/product-option-image/delete/{productsOfferFlashId}")]
        public async Task<IActionResult> CreateAsync([FromRoute] string productsOfferFlashId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productOptionImageService.DeleteAllByProductsOfferFlashId(Guid.Parse(productsOfferFlashId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
