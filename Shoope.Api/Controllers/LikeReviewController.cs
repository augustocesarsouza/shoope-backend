using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class LikeReviewController : ControllerBase
    {
        public readonly ILikeReviewService _likeReviewService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public LikeReviewController(ILikeReviewService likeReviewService, IBaseController baseController, ICurrentUser currentUser)
        {
            _likeReviewService = likeReviewService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/like-review/get-by-product-flash-sale-reviews-id/{productFlashSaleReviewsId}")]
        public async Task<IActionResult> GetAddressById([FromRoute] string productFlashSaleReviewsId)
        {
            var result = await _likeReviewService.GetByProductFlashSaleReviewsId(Guid.Parse(productFlashSaleReviewsId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/like-review/create")]
        public async Task<IActionResult> CreateAsync([FromBody] LikeReviewDTO likeReviewDTO)
        {
            var result = await _likeReviewService.CreateAsync(likeReviewDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/like-review/delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] LikeReviewDTO likeReviewDTO)
        {
            var result = await _likeReviewService.DeleteAsync(likeReviewDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
