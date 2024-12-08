using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductHighlightRepository : IGenericRepository<ProductHighlight>
    {
        public Task<ProductHighlight?> GetProductHighlightById(Guid productHighlightId);
        public Task<List<ProductHighlight>?> GetAllProductHighlight();
    }
}
