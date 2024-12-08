using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class ProductDescriptionController : ControllerBase
    {
        public readonly IProductDescriptionService _productDescriptionService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductDescriptionController(IProductDescriptionService productDescriptionService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productDescriptionService = productDescriptionService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/product-description/get-product-description-by-product-id/{productId}")]
        public async Task<IActionResult> GetProductDescriptionByProductId([FromRoute] string productId)
        {
            var userAuth = _baseController.Validator(_currentUser);

            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _productDescriptionService.GetProductDescriptionByProductId(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/product-description/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDescriptionDTO productDescriptionDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productDescriptionService.CreateAsync(productDescriptionDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
