using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductDiscoveriesOfDayRepository : IGenericRepository<ProductDiscoveriesOfDay>
    {
        public Task<ProductDiscoveriesOfDay?> GetProductDiscoveriesOfDayById(Guid productDiscoveriesOfDayId);
        public Task<List<ProductDiscoveriesOfDay>?> GetAllProductDiscoveriesOfDay();
    }
}
