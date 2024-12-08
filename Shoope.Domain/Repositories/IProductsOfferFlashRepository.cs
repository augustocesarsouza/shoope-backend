using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductsOfferFlashRepository : IGenericRepository<ProductsOfferFlash>
    {
        public Task<List<ProductsOfferFlash>> GetAllProduct();
        public Task<List<ProductsOfferFlash>> GetAllByTagProduct(string hourFlashOffer, string tagProduct, int pageNumber, int pageSize);
    }
}
