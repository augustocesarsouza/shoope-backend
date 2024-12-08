using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class PromotionUserController : ControllerBase
    {
        public readonly IPromotionUserService _promotionUserService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public PromotionUserController(IPromotionUserService promotionUserService, IBaseController baseController, ICurrentUser currentUser)
        {
            _promotionUserService = promotionUserService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        //[Authorize]
        //[HttpGet("v1/promotion-user/get-address-by-user-id/{userId}")]
        //public async Task<IActionResult> GetByUserId([FromRoute] string userId)
        //{
        //    var userAuth = _baseController.Validator(_currentUser);
        //    if (userAuth == null)
        //        return _baseController.Forbidden();

        //    var result = await _promotionUserService.GetById(Guid.Parse(userId));

        //    if (result.IsSucess)
        //        return Ok(result);

        //    return BadRequest(result);
        //}

        [Authorize]
        [HttpGet("v1/promotion-user/get-by-user-id-all/{userId}")]
        public async Task<IActionResult> GetByUserIdAll([FromRoute] string userId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _promotionUserService.GetByUserIdAll(Guid.Parse(userId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //public async Task<ResultService<List<PromotionUserDTO>>> GetByUserIdAll(Guid guidId)

        [HttpPost("v1/promotion-user/create")]
        public async Task<IActionResult> CreateAsync([FromBody] PromotionUserDTO promotionUserDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _promotionUserService.Create(promotionUserDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
