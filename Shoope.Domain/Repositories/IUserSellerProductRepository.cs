using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IUserSellerProductRepository : IGenericRepository<UserSellerProduct>
    {
        public Task<UserSellerProduct?> GetById(Guid userSellerProductId);
    }
}
