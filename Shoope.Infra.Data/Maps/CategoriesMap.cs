using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class CategoriesMap : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable("tb_categories");

            builder.HasKey(e => e.Id)
                .HasName("pk_categories");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("categories_id");

            builder.Property(e => e.ImgCategory)
                .IsRequired()
               .HasColumnName("img_category");

            builder.Property(e => e.ImgCategoryPublicId)
                .IsRequired()
               .HasColumnName("img_category_public_id");

            builder.Property(e => e.AltValue)
                .IsRequired()
               .HasColumnName("alt_value");

            builder.Property(e => e.Title)
                .IsRequired()
               .HasColumnName("title");
        }
    }
}
