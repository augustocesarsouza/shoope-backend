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
    public class PromotionController : ControllerBase
    {
        public readonly IPromotionService _promotionService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public PromotionController(IPromotionService promotionService, IBaseController baseController, ICurrentUser currentUser)
        {
            _promotionService = promotionService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpPost("v1/promotion/create")]
        public async Task<IActionResult> CreateAsync([FromBody] PromotionDTO promotionDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _promotionService.Create(promotionDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/promotion/delete/{promotionId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string promotionId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _promotionService.DeletePromotion(Guid.Parse(promotionId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
