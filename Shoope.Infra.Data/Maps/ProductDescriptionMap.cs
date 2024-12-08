using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductDescriptionMap : IEntityTypeConfiguration<ProductDescription>
    {
        public void Configure(EntityTypeBuilder<ProductDescription> builder)
        {
            builder.ToTable("tb_product_descriptions");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_descriptions");

            builder.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnType("uuid")
                    .HasColumnName("product_descriptions_id");

            builder.Property(e => e.Description)
                .IsRequired(true)
               .HasColumnName("description");

            builder.Property(e => e.Characteristics)
                .IsRequired(true)
                .HasColumnType("text[]")
               .HasColumnName("characteristics");

            builder.Property(e => e.ProductId)
                .IsRequired(true)
               .HasColumnName("product_id");
        }
    }
}
