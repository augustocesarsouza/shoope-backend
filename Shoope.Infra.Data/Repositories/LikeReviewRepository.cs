using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class LikeReviewRepository : GenericRepository<LikeReview>, ILikeReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LikeReview>> GetByProductFlashSaleReviewsId(Guid productFlashSaleReviewsId)
        {
            var likePreviews = await _context
                .LikeReviews
                .Where(x => x.ProductFlashSaleReviewsId == productFlashSaleReviewsId)
                .ToListAsync();

            return likePreviews;
        }

        public async Task<LikeReview?> GetByUserId(Guid? userId)
        {
            var likePreview = await _context
                .LikeReviews
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            return likePreview;
        }

        public async Task<LikeReview?> AlreadyExistLike(Guid? userId, Guid? productFlashSaleReviewsId)
        {
            var likePreview = await _context
                .LikeReviews
                .Where(x => x.UserId == userId && x.ProductFlashSaleReviewsId == productFlashSaleReviewsId)
                .FirstOrDefaultAsync();

            return likePreview;
        }
    }
}
