using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class ProductFlashSaleReviewsController : ControllerBase
    {
        public readonly IProductFlashSaleReviewsService _productFlashSaleReviewsService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductFlashSaleReviewsController(IProductFlashSaleReviewsService productFlashSaleReviewsService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productFlashSaleReviewsService = productFlashSaleReviewsService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/product-flash-sale-reviews/get-all-product-flash-sale-reviews-by-product-flash-sale-id/{productFlashSaleId}")]
        public async Task<IActionResult> GetAllProductFlashSaleReviewsByProductFlashSaleId([FromRoute] string productFlashSaleId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _productFlashSaleReviewsService.GetAllProductFlashSaleReviewsByProductFlashSaleId(Guid.Parse(productFlashSaleId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/product-flash-sale-reviews/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductFlashSaleReviewsDTO productFlashSaleReviewsDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productFlashSaleReviewsService.CreateAsync(productFlashSaleReviewsDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/product-flash-sale-reviews/delete/{productFlashSaleReviewsId}")]
        public async Task<IActionResult> Delete([FromRoute] string productFlashSaleReviewsId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productFlashSaleReviewsService.Delete(Guid.Parse(productFlashSaleReviewsId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
