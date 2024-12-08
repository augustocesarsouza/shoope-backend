using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class FlashSaleProductAllInfoMap : IEntityTypeConfiguration<FlashSaleProductAllInfo>
    {
        public void Configure(EntityTypeBuilder<FlashSaleProductAllInfo> builder)
        {
            builder.ToTable("tb_flash_sale_product_all_infos");

            builder.HasKey(e => e.Id)
                .HasName("pk_flash_sale_product_all_infos");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("flash_sale_product_all_infos_id");

            //builder.Property(e => e.ProductFlashSaleId)
            //    .IsRequired(true)
            //   .HasColumnName("product_flash_sale_id");

            builder.Property(e => e.ProductReviewsRate)
               .IsRequired(true)
               .HasColumnName("product_reviews_rate");

            builder.Property(e => e.QuantitySold)
              .IsRequired(true)
              .HasColumnName("quantity_sold");

            builder.Property(e => e.FavoriteQuantity)
              .IsRequired(true)
              .HasColumnName("favorite_quantity");

            builder.Property(e => e.QuantityAvaliation)
              .IsRequired(true)
              .HasColumnName("quantity_avaliation");

            builder.Property(e => e.Coins)
              .IsRequired(false)
              .HasColumnName("coins");

            builder.Property(e => e.CreditCard)
              .IsRequired(false)
              .HasColumnName("credit_card");

            builder.Property(e => e.Voltage)
              .IsRequired(false)
              .HasColumnName("voltage");

            builder.Property(e => e.QuantityPiece)
              .IsRequired(false)
              .HasColumnName("quantity_piece");

            builder.Property(e => e.Size)
              .IsRequired(false)
              .HasColumnName("size");

            builder.Property(e => e.ProductHaveInsurance)
              .IsRequired(false)
              .HasColumnName("product_have_insurance")
              .HasDefaultValue(false);

            builder.Property(e => e.ProductsOfferFlashId)
                .IsRequired(true)
               .HasColumnName("products_offer_flash_id");

            builder.HasOne(x => x.ProductsOfferFlash)
                .WithMany()
                .HasForeignKey(x => x.ProductsOfferFlashId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Ignore(e => e.ProductsOfferFlash);
        }
    }
}
