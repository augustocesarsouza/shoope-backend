using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IPromotionRepository
    {
        public Task<Promotion?> GetById(Guid promotionId);
        public Task<Promotion?> Create(Promotion promotion);
        public Task<Promotion> Delete(Promotion promotion);
    }
}
