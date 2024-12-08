using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductsOfferFlashService
    {
        public Task<ResultService<List<ProductsOfferFlashDTO>>> GetAllProduct();
        public Task<ResultService<List<ProductsOfferFlashDTO>>> GetAllByTagProduct(string hourFlashOffer, string tagProduct, int pageNumber, int pageSize);
        public Task<ResultService<ProductsOfferFlashDTO>> CreateAsync(ProductsOfferFlashDTO? productDTO);
    }
}
