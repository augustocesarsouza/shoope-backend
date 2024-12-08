using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductHighlightRepository : GenericRepository<ProductHighlight>, IProductHighlightRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductHighlightRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductHighlight?> GetProductHighlightById(Guid productHighlightId)
        {
            var productHighlight = await _context
              .ProductHighlights
              .Where(x => x.Id == productHighlightId)
              .FirstOrDefaultAsync();

            return productHighlight;
        }

        public async Task<List<ProductHighlight>?> GetAllProductHighlight()
        {
            var productHighlights = await _context
               .ProductHighlights
               .Select(x => new ProductHighlight(x.Id, x.Title, x.ImgProduct, null, x.ImgTop, x.QuantitySold))
               .ToListAsync();

            return productHighlights;
        }
    }
}
