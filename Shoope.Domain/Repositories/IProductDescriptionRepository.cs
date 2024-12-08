using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductDescriptionRepository : IGenericRepository<ProductDescription>
    {
        public Task<ProductDescription?> GetProductDescriptionByProductId(Guid productId);
    }
}
