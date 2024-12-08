using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductSellerRepository : IGenericRepository<ProductSeller>
    {
        public Task<ProductSeller?> GetById(Guid userSellerProductId);
    }
}
