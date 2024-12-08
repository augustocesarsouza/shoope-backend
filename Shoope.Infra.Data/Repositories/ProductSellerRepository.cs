using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class ProductSellerRepository : GenericRepository<ProductSeller>, IProductSellerRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductSellerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductSeller?> GetById(Guid productId)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById(localTimeZone.Id);

            //var user = await _context.ProductSellers
            //   .Where(x => x.ProductId == productId)
            //   .Select(x => new ProductSeller(null, null,
            //    x.UserSellerProduct != null ? new UserSellerProduct(x.UserSellerProduct.Id, x.UserSellerProduct.Name, x.UserSellerProduct.ImgPerfil,
            //   null, x.UserSellerProduct.ImgFloating, null, x.UserSellerProduct.LastLogin, x.UserSellerProduct.Reviews, x.UserSellerProduct.ChatResponseRate, 
            //   x.UserSellerProduct.AccountCreationDate, x.UserSellerProduct.QuantityOfProductSold, x.UserSellerProduct.UsuallyRespondsToChatIn, x.UserSellerProduct.Followers) 
            //    : null
            //   , null))
            //   .FirstOrDefaultAsync();

            var user = await _context.ProductSellers
               .Where(x => x.ProductId == productId)
               .Select(x => new ProductSeller(null, null,
                x.UserSellerProduct != null ? new UserSellerProduct(x.UserSellerProduct.Id, x.UserSellerProduct.Name, x.UserSellerProduct.ImgPerfil,
               null, x.UserSellerProduct.ImgFloating, null, x.UserSellerProduct.LastLogin.HasValue ?
               TimeZoneInfo.ConvertTimeFromUtc(x.UserSellerProduct.LastLogin.Value, brazilTimeZone) : null, x.UserSellerProduct.Reviews, x.UserSellerProduct.ChatResponseRate,
               x.UserSellerProduct.AccountCreationDate.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(x.UserSellerProduct.AccountCreationDate.Value, brazilTimeZone) : null,
               x.UserSellerProduct.QuantityOfProductSold, x.UserSellerProduct.UsuallyRespondsToChatIn, x.UserSellerProduct.Followers)
                : null
               , null))
               .FirstOrDefaultAsync();

            return user;
        }
    }
}
