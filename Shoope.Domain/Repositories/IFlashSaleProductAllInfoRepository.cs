using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IFlashSaleProductAllInfoRepository : IGenericRepository<FlashSaleProductAllInfo>
    {
        public Task<FlashSaleProductAllInfo?> GetFlashSaleProductByProductFlashSaleId(Guid productFlashSaleId);
    }
}
