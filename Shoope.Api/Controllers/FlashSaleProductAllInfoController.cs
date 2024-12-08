using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class FlashSaleProductAllInfoController : ControllerBase
    {
        public readonly IFlashSaleProductAllInfoService _flashSaleProductAllInfoService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public FlashSaleProductAllInfoController(IFlashSaleProductAllInfoService flashSaleProductAllInfoService, IBaseController baseController, ICurrentUser currentUser)
        {
            _flashSaleProductAllInfoService = flashSaleProductAllInfoService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/get-flash-sale-product-by-product-flash-sale-id/{productFlashSaleId}")]
        public async Task<IActionResult> GetFlashSaleProductByProductFlashSaleId([FromRoute] string productFlashSaleId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _flashSaleProductAllInfoService.GetFlashSaleProductByProductFlashSaleId(Guid.Parse(productFlashSaleId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/flash-sale-product-all-info/create")]
        public async Task<IActionResult> CreateAsync([FromBody] FlashSaleProductAllInfoDTO flashSaleProductAllInfoDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _flashSaleProductAllInfoService.CreateAsync(flashSaleProductAllInfoDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
