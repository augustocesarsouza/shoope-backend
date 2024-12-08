using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;
using System.Linq;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductDescriptionRepository : GenericRepository<ProductDescription>, IProductDescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductDescriptionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductDescription?> GetProductDescriptionByProductId(Guid productId)
        {
            var productDetail = await _context.ProductDescriptions
               .Where(x => x.ProductId == productId)
               .Select(x => new ProductDescription(x.Id, x.Description, x.Characteristics, null))
               .FirstOrDefaultAsync();

            return productDetail;
        }
    }
}
