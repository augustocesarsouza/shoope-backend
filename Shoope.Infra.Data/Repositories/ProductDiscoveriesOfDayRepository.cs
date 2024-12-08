using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductDiscoveriesOfDayRepository : GenericRepository<ProductDiscoveriesOfDay>, IProductDiscoveriesOfDayRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductDiscoveriesOfDayRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductDiscoveriesOfDay?> GetProductDiscoveriesOfDayById(Guid productDiscoveriesOfDayId)
        {
            var productHighlight = await _context
                .ProductDiscoveriesOfDays
                .Where(x => x.Id == productDiscoveriesOfDayId)
                .FirstOrDefaultAsync();

            return productHighlight;
        }

        public async Task<List<ProductDiscoveriesOfDay>?> GetAllProductDiscoveriesOfDay()
        {
            var productHighlightAll = await _context
                 .ProductDiscoveriesOfDays
                 .Select(x => new ProductDiscoveriesOfDay(x.Id, x.Title, x.ImgProduct, null, x.ImgPartBottom, x.DiscountPercentage, x.IsAd, x.Price, x.QuantitySold))
                 .ToListAsync();

            return productHighlightAll;
        }
    }
}
