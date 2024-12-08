using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductsOfferFlashMap : IEntityTypeConfiguration<ProductsOfferFlash>
    {
        public void Configure(EntityTypeBuilder<ProductsOfferFlash> builder)
        {
            builder.ToTable("tb_products_offer_flash");

            builder.HasKey(e => e.Id)
                .HasName("pk_products_offer_flash");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("products_offer_flash_id");

            builder.Property(e => e.ImgProduct)
               .IsRequired(true)
               .HasColumnName("img_product");

            builder.Property(e => e.ImgProductPublicId)
               .IsRequired(true)
              .HasColumnName("img_product_public_id");

            builder.Property(e => e.AltValue)
               .IsRequired(true)
               .HasColumnName("alt_value");

            builder.Property(e => e.ImgPartBottom)
               .IsRequired(false)
               .HasColumnName("img_part_bottom");

            //builder.Property(e => e.ImgPartBottomPublicId)
            //   .IsRequired(false)
            //   .HasColumnName("img_part_bottom_public_id");

            builder.Property(e => e.PriceProduct)
               .IsRequired(true)
               .HasColumnName("price_product");

            builder.Property(e => e.PopularityPercentage)
                .IsRequired(true)
               .HasColumnName("popularity_percentage");

            builder.Property(e => e.DiscountPercentage)
                .IsRequired(true)
               .HasColumnName("discount_percentage");

            builder.Property(e => e.HourFlashOffer)
                .IsRequired(true)
               .HasColumnName("hour_flash_offer");

            builder.Property(e => e.Title)
               .IsRequired(true)
              .HasColumnName("title");

            builder.Property(e => e.TagProduct)
              .IsRequired(true)
             .HasColumnName("tag_product");

            //builder.Property(e => e.Type)
            //    .HasConversion<string>();
        }
    }
}
