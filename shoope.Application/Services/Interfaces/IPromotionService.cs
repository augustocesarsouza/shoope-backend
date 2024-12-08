using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IPromotionService
    {
        public Task<ResultService<PromotionDTO>> GetById(Guid promotionId);
        public Task<ResultService<PromotionDTO>> Create(PromotionDTO? promotionDTO);
        public Task<ResultService<PromotionDTO>> DeletePromotion(Guid promotionId);
    }
}
