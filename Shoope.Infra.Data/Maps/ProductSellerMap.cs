using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductSellerMap : IEntityTypeConfiguration<ProductSeller>
    {
        public void Configure(EntityTypeBuilder<ProductSeller> builder)
        {
            builder.ToTable("tb_product_sellers");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_sellers");

            builder.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnType("uuid")
                .HasColumnName("product_sellers_id");

            builder.Property(e => e.UserSellerProductId)
                .IsRequired(true)
               .HasColumnName("user_seller_product_id");

            builder.HasIndex(e => e.UserSellerProductId)
               .HasDatabaseName("ix_product_seller_user_seller_product_id");

            builder.Property(e => e.ProductId)
                .IsRequired(true)
               .HasColumnName("product_id");

            builder.HasOne(x => x.UserSellerProduct);
        }
    }
}
