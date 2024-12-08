using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Users = Set<User>();
            Addresses = Set<Address>();
            Promotion = Set<Promotion>();
            PromotionUser = Set<PromotionUser>();
            Cupons = Set<Cupon>();
            UserCupons = Set<UserCupon>();
            Products = Set<ProductsOfferFlash>();
            Categories = Set<Categories>();
            ProductHighlights = Set<ProductHighlight>();
            ProductDiscoveriesOfDays = Set<ProductDiscoveriesOfDay>();
            FlashSaleProductAllInfos = Set<FlashSaleProductAllInfo>();
            ProductFlashSaleReviews = Set<ProductFlashSaleReviews>();
            UserSellerProducts = Set<UserSellerProduct>();
            ProductSellers = Set<ProductSeller>();
            ProductOptionImages = Set<ProductOptionImage>();
            ProductDetails = Set<ProductDetail>();
            ProductDescriptions = Set<ProductDescription>();
            LikeReviews = Set<LikeReview>();
        }
        
        public DbSet<User> Users { get; private set; }
        public DbSet<Address> Addresses { get; private set; }
        public DbSet<Promotion> Promotion { get; private set; }
        public DbSet<PromotionUser> PromotionUser { get; private set; }
        public DbSet<Cupon> Cupons { get; private set; }
        public DbSet<UserCupon> UserCupons { get; private set; }
        public DbSet<ProductsOfferFlash> Products { get; private set; }
        public DbSet<Categories> Categories { get; private set; }
        public DbSet<ProductHighlight> ProductHighlights { get; private set; }
        public DbSet<ProductDiscoveriesOfDay> ProductDiscoveriesOfDays { get; private set; }
        public DbSet<FlashSaleProductAllInfo> FlashSaleProductAllInfos { get; private set; }
        public DbSet<ProductFlashSaleReviews> ProductFlashSaleReviews { get; private set; }
        public DbSet<UserSellerProduct> UserSellerProducts { get; private set; }
        public DbSet<ProductSeller> ProductSellers { get; private set; }
        public DbSet<ProductOptionImage> ProductOptionImages { get; private set; }
        public DbSet<ProductDetail> ProductDetails { get; private set; }
        public DbSet<ProductDescription> ProductDescriptions { get; private set; }
        public DbSet<LikeReview> LikeReviews { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
