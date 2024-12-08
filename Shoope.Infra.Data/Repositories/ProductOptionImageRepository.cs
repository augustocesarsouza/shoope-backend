using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;
using System.Linq;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductOptionImageRepository : GenericRepository<ProductOptionImage>, IProductOptionImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductOptionImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductOptionImage>> GetByListFlashSaleProductImageAllId(Guid productsOfferFlashId)
        {
            var productOptionImagesAll = await _context.ProductOptionImages
                .Where(x => x.ProductsOfferFlashId == productsOfferFlashId)
                .Select(x => new ProductOptionImage(x.Id, x.OptionType, x.ImageUrl, null, null, x.ImgAlt, x.TitleOptionType))
                .ToListAsync();

            return productOptionImagesAll;
        }

        public async Task<List<ProductOptionImage>> GetAllByProductsOfferFlashId(Guid productsOfferFlashId)
        {
            var productOptionImagesAll = await _context.ProductOptionImages
                .Where(x => x.ProductsOfferFlashId == productsOfferFlashId)
                .ToListAsync();

            return productOptionImagesAll;
        }
    }
}
