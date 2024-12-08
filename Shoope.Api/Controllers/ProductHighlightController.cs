using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class ProductHighlightController : ControllerBase
    {
        public readonly IProductHighlightService _productHighlightService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductHighlightController(IProductHighlightService productHighlightService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productHighlightService = productHighlightService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/get-product-highlight-by-id/{categoryId}")]
        public async Task<IActionResult> GetCategoriesById([FromRoute] string productHighlightId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productHighlightService.GetProductHighlightById(Guid.Parse(productHighlightId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/get-all-product-highlights")]
        public async Task<IActionResult> GetAllCategories()
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _productHighlightService.GetAllProductHighlights();

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/product-highlights/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductHighlightDTO productHighlightDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productHighlightService.CreateAsync(productHighlightDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
