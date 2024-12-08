using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface ILikeReviewService
    {
        public Task<ResultService<List<LikeReviewDTO>>> GetByProductFlashSaleReviewsId(Guid productFlashSaleReviewsId);
        public Task<ResultService<LikeReviewDTO>> CreateAsync(LikeReviewDTO? likeReviewDTO);
        public Task<ResultService<LikeReviewDTO>> DeleteAsync(LikeReviewDTO? likeReviewDTO);
    }
}
