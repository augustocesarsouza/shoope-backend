using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductDetailRepository : IGenericRepository<ProductDetail>
    {
        public Task<ProductDetail?> GetProductDetailByProductId(Guid productId);
    }
}
