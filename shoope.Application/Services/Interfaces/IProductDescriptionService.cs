using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductDescriptionService
    {
        public Task<ResultService<ProductDescriptionDTO>> GetProductDescriptionByProductId(Guid productId);
        public Task<ResultService<ProductDescriptionDTO>> CreateAsync(ProductDescriptionDTO? productDescriptionDTO);
    }
}
