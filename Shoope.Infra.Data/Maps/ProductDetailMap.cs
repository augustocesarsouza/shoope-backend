using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductDetailMap : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.ToTable("tb_product_details");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_details");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_details_id");

            builder.Property(e => e.PromotionalStock)
                .IsRequired(true)
               .HasColumnName("promotional_stock");

            builder.Property(e => e.TotalStock)
                .IsRequired(true)
               .HasColumnName("total_stock");

            builder.Property(e => e.SendingOf)
                .IsRequired(true)
               .HasColumnName("sending_of");

            builder.Property(e => e.Mark)
                .IsRequired(false)
               .HasColumnName("mark");

            builder.Property(e => e.Gender)
                .IsRequired(false)
               .HasColumnName("gender");

            builder.Property(e => e.WarrantlyDuration)
                .IsRequired(false)
               .HasColumnName("warrantly_duration");

            builder.Property(e => e.WarrantlyType)
                .IsRequired(false)
               .HasColumnName("warrantly_type");

            builder.Property(e => e.ProductWeight)
                .IsRequired(false)
               .HasColumnName("product_weight");

            builder.Property(e => e.EnergyConsumption)
                .IsRequired(false)
               .HasColumnName("energy_consumption");

            builder.Property(e => e.Amount)
                .IsRequired(false)
               .HasColumnName("amount");

            builder.Property(e => e.Material)
                .IsRequired(false)
               .HasColumnName("material");

            builder.Property(e => e.ProductId)
                .IsRequired(true)
               .HasColumnName("product_id");
        }
    }
}
