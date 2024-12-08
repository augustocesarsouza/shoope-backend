using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductDetailService
    {
        public Task<ResultService<ProductDetailDTO>> GetProductDetailByProductId(Guid productId);
        public Task<ResultService<ProductDetailDTO>> CreateAsync(ProductDetailDTO? productDetailDTO);
    }
}
