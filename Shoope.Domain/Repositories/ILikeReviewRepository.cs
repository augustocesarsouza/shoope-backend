using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface ILikeReviewRepository : IGenericRepository<LikeReview>
    {
        public Task<List<LikeReview>> GetByProductFlashSaleReviewsId(Guid productFlashSaleReviewsId);
        public Task<LikeReview?> GetByUserId(Guid? userId);
        public Task<LikeReview?> AlreadyExistLike(Guid? userId, Guid? productFlashSaleReviewsId);
    }
}
