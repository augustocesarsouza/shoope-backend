using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductFlashSaleReviewsRepository : IGenericRepository<ProductFlashSaleReviews>
    {
        public Task<ProductFlashSaleReviews?> GetByProductFlashSaleId(Guid productFlashSaleReviewsId);
        public Task<List<ProductFlashSaleReviews>?> GetAllProductFlashSaleReviewsByProductFlashSaleId(Guid productFlashSaleId);
    }
}