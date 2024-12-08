using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductFlashSaleReviewsMap : IEntityTypeConfiguration<ProductFlashSaleReviews>
    {
        public void Configure(EntityTypeBuilder<ProductFlashSaleReviews> builder)
        {
            builder.ToTable("tb_product_flash_sale_reviews");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_flash_sale_reviews");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_flash_sale_reviews_id");

            builder.Property(e => e.Message)
                .IsRequired(true)
               .HasColumnName("message");

            builder.Property(e => e.CreationDate)
               .IsRequired(true)
               .HasColumnName("creation_date");

            builder.Property(e => e.CostBenefit)
              .IsRequired(true)
              .HasColumnName("cost_benefit");

            builder.Property(e => e.SimilarToAd)
               .IsRequired(true)
               .HasColumnName("similar_to_ad");

            builder.Property(e => e.StarQuantity)
              .IsRequired(true)
              .HasColumnName("star_quantity");

            builder.Property(e => e.ImgAndVideoReviewsProduct)
              .IsRequired(false)
              .HasColumnType("text[]")
              .HasColumnName("img_and_video_reviews_product");

            builder.Property(e => e.ProductsOfferFlashId)
                .IsRequired(true)
               .HasColumnName("products_offer_flash_id");

            builder.Property(e => e.Variation)
                .IsRequired(false)
               .HasColumnName("variation");

            builder.Property(e => e.UserId)
                .IsRequired(true)
               .HasColumnName("user_id");


            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
