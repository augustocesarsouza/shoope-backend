using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class FlashSaleProductAllInfoRepository : GenericRepository<FlashSaleProductAllInfo>, IFlashSaleProductAllInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public FlashSaleProductAllInfoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FlashSaleProductAllInfo?> GetFlashSaleProductByProductFlashSaleId(Guid productFlashSaleId)
        {
            var flashSaleProductAllInfo = await _context.FlashSaleProductAllInfos
                .Where(x => x.ProductsOfferFlashId == productFlashSaleId)
                .Select(x => new FlashSaleProductAllInfo(x.Id, null, x.ProductReviewsRate, x.QuantitySold,
                x.ProductsOfferFlash != null ? new ProductsOfferFlash(x.ProductsOfferFlash.Id, x.ProductsOfferFlash.ImgProduct, null, x.ProductsOfferFlash.AltValue, x.ProductsOfferFlash.ImgPartBottom,
                x.ProductsOfferFlash.PriceProduct, null, x.ProductsOfferFlash.DiscountPercentage, null, x.ProductsOfferFlash.Title, null) 
                : null, x.FavoriteQuantity, x.QuantityAvaliation, x.Coins, x.CreditCard, x.Voltage, x.QuantityPiece, x.Size, x.ProductHaveInsurance
                ))
                .FirstOrDefaultAsync();

            return flashSaleProductAllInfo;
        }
    }
}
