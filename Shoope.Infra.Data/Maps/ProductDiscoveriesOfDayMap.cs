using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductDiscoveriesOfDayMap : IEntityTypeConfiguration<ProductDiscoveriesOfDay>
    {
        public void Configure(EntityTypeBuilder<ProductDiscoveriesOfDay> builder)
        {
            builder.ToTable("tb_product_discoveries_of_days");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_discoveries_of_days");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_discoveries_of_days_id");

            builder.Property(e => e.Title)
                .IsRequired()
               .HasColumnName("title");

            builder.Property(e => e.ImgProduct)
                .IsRequired()
               .HasColumnName("img_product");

            builder.Property(e => e.ImgProductPublicId)
                .IsRequired()
               .HasColumnName("img_product_public_id");
            
            builder.Property(e => e.ImgPartBottom)
                .IsRequired(false)
               .HasColumnName("img_part_bottom");

            builder.Property(e => e.DiscountPercentage)
                .IsRequired(false)
               .HasColumnName("discount_percentage");

            builder.Property(e => e.IsAd)
                .IsRequired()
               .HasColumnName("is_ad");

            builder.Property(e => e.Price)
                .IsRequired()
               .HasColumnName("price");

            builder.Property(e => e.QuantitySold)
                .IsRequired(false)
               .HasColumnName("quantity_sold");
        }
    }
}
