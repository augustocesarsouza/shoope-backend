using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductsOfferFlashRepository : GenericRepository<ProductsOfferFlash>, IProductsOfferFlashRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductsOfferFlashRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductsOfferFlash>> GetAllProduct()
        {
            var product = await _context
                .Products
                .Select(x => new ProductsOfferFlash(x.Id, x.ImgProduct, null, x.AltValue,x.ImgPartBottom ,x.PriceProduct, 
                x.PopularityPercentage, x.DiscountPercentage, null, null, null))
                .ToListAsync();

            return product;
        }

        public async Task<List<ProductsOfferFlash>> GetAllByTagProduct(string hourFlashOffer, string tagProduct, int pageNumber, int pageSize)
        {
            var product = await _context
                .Products
                .Where(x => x.TagProduct == tagProduct && x.HourFlashOffer == hourFlashOffer)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ProductsOfferFlash(x.Id, x.ImgProduct, null, x.AltValue, x.ImgPartBottom, x.PriceProduct,
                x.PopularityPercentage, x.DiscountPercentage, null, x.Title, x.TagProduct))
                .ToListAsync();

            return product;
        }
    }
}
