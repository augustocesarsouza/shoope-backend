using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductDetailRepository : GenericRepository<ProductDetail>, IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductDetail?> GetProductDetailByProductId(Guid productId)
        {
            var productDetail = await _context.ProductDetails
               .Where(x => x.ProductId == productId)
               .Select(x => new ProductDetail(x.Id, x.PromotionalStock, x.TotalStock, x.Mark, x.Gender, x.WarrantlyDuration, x.WarrantlyType, x.ProductWeight,
               x.EnergyConsumption, x.SendingOf, x.Amount, x.Material, null))
               .FirstOrDefaultAsync();

            return productDetail;
        }
    }
}