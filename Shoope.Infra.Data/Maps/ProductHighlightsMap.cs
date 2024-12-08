using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductHighlightsMap : IEntityTypeConfiguration<ProductHighlight>
    {
        public void Configure(EntityTypeBuilder<ProductHighlight> builder)
        {
            builder.ToTable("tb_product_highlights");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_highlights");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_highlights_id");

            builder.Property(e => e.Title)
                .IsRequired()
               .HasColumnName("title");

            builder.Property(e => e.ImgProduct)
                .IsRequired()
               .HasColumnName("img_product");

            builder.Property(e => e.ImgProductPublicId)
                .IsRequired()
               .HasColumnName("img_product_public_id");

            builder.Property(e => e.ImgTop)
                .IsRequired()
               .HasColumnName("img_top");

            builder.Property(e => e.QuantitySold)
                .IsRequired()
               .HasColumnName("quantity_sold");
        }
    }
}
