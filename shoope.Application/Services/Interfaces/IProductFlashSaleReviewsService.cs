using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductFlashSaleReviewsService
    {
        public Task<ResultService<List<ProductFlashSaleReviewsDTO>?>> GetAllProductFlashSaleReviewsByProductFlashSaleId(Guid productFlashSaleId);
        public Task<ResultService<ProductFlashSaleReviewsDTO>> CreateAsync(ProductFlashSaleReviewsDTO? productFlashSaleReviewsDTO);
        public Task<ResultService<ProductFlashSaleReviewsDTO>> Delete(Guid promotionId);
    }
}
