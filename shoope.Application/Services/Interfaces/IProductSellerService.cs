using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductSellerService
    {
        public Task<ResultService<ProductSellerDTO>> GetById(Guid productId);
        public Task<ResultService<ProductSellerDTO>> Create(ProductSellerDTO? productSellerDTO);
    }
}
