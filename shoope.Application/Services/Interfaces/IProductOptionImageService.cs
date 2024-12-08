using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductOptionImageService
    {
        public Task<ResultService<List<ProductOptionImageDTO>>> GetByListFlashSaleProductImageAllId(Guid productsOfferFlashId);
        public Task<ResultService<ProductOptionImageDTO>> Create(ProductOptionImageDTO? productOptionImageDTO);
        public Task<ResultService<string>> DeleteAllByProductsOfferFlashId(Guid productsOfferFlashId);
    }
}
