using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IPromotionUserRepository : IGenericRepository<PromotionUser>
    {
        //public Task<List<PromotionUser>?> GetByMovieId(Guid idMovie);
        public Task<List<PromotionUser>> GetById(Guid guidId);
        public Task<List<PromotionUser>> GetByUserIdAll(Guid userId);
        public Task<List<PromotionUser>> GetPromotionUserByPromotionId(Guid promotionId);
    }
}
