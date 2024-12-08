using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IProductOptionImageRepository : IGenericRepository<ProductOptionImage>
    {
        public Task<List<ProductOptionImage>> GetByListFlashSaleProductImageAllId(Guid productsOfferFlashId);
        public Task<List<ProductOptionImage>> GetAllByProductsOfferFlashId(Guid productsOfferFlashId);
    }
}
