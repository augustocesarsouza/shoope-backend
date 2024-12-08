using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductFlashSaleReviewsRepository : GenericRepository<ProductFlashSaleReviews>, IProductFlashSaleReviewsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductFlashSaleReviewsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductFlashSaleReviews?> GetByProductFlashSaleId(Guid productFlashSaleReviewsId)
        {
            var product = await _context
                .ProductFlashSaleReviews
                .Where(x => x.Id == productFlashSaleReviewsId)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<List<ProductFlashSaleReviews>?> GetAllProductFlashSaleReviewsByProductFlashSaleId(Guid productFlashSaleId)
        {
            //var allProduct = await _context
            //    .ProductFlashSaleReviews
            //    .Where(x => x.ProductsOfferFlashId == productFlashSaleId)
            //    .Select(x => new ProductFlashSaleReviews(x.Id, x.Message, 
            //    x.CreationDate, x.CostBenefit, x.SimilarToAd, x.StarQuantity, null))
            //    .ToListAsync();

            //return allProduct;

            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById(localTimeZone.Id);

            var allProduct = await _context
                .ProductFlashSaleReviews
                .Where(x => x.ProductsOfferFlashId == productFlashSaleId)
                .Select(x => new ProductFlashSaleReviews(x.Id, x.Message,
                x.CreationDate.HasValue
                ? TimeZoneInfo.ConvertTimeFromUtc(x.CreationDate.Value, brazilTimeZone)
                : (DateTime?)null,
                x.CostBenefit, x.SimilarToAd, x.StarQuantity, null, null, 
                x.User != null ? new User(null, x.User.Name, null, null, null, null, null, null, null, x.User.UserImage) : null, 
                x.ImgAndVideoReviewsProduct, x.Variation))
                .ToListAsync();

            return allProduct;
        }
    }
}
