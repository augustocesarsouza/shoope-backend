using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class ProductsOfferFlashController : ControllerBase
    {
        public readonly IProductsOfferFlashService _productService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductsOfferFlashController(IProductsOfferFlashService productService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productService = productService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/get-product-offer-flash-all")]
        public async Task<IActionResult> GetAllProduct()
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _productService.GetAllProduct();

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpGet("v1/get-all-by-tag-product/{hourFlashOffer}/{tagProduct}/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAllByTagProduct([FromRoute] string hourFlashOffer, [FromRoute] string tagProduct, [FromRoute] string pageNumber, [FromRoute] string pageSize)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productService.GetAllByTagProduct(hourFlashOffer, tagProduct, int.Parse(pageNumber), int.Parse(pageSize));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/product-offer-flash/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductsOfferFlashDTO productDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productService.CreateAsync(productDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
