using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IPromotionUserService
    {
        public Task<ResultService<List<PromotionUserDTO>>> GetByUserIdAll(Guid guidId);
        public Task<ResultService<PromotionUserDTO>> Create(PromotionUserDTO? promotionUserDTO);
        public Task<ResultService<string>> Delete(Guid promotionId);
    }
}
