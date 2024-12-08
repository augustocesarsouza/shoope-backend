using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class ProductOptionImageMap : IEntityTypeConfiguration<ProductOptionImage>
    {
        public void Configure(EntityTypeBuilder<ProductOptionImage> builder)
        {
            builder.ToTable("tb_product_option_images");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_option_images");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_option_images_id");

            builder.Property(e => e.OptionType)
                .IsRequired(true)
               .HasColumnName("option_type");

            builder.Property(e => e.ImgAlt)
               .IsRequired(true)
              .HasColumnName("img_alt");

            builder.Property(e => e.ImageUrl)
                .IsRequired(true)
               .HasColumnName("image_url");

            builder.Property(e => e.PublicId)
                .IsRequired(true)
               .HasColumnName("public_id");

            builder.Property(e => e.TitleOptionType)
                .IsRequired(false)
               .HasColumnName("title_option_type");

            builder.Property(e => e.ProductsOfferFlashId)
                .IsRequired(true)
               .HasColumnName("products_offer_flash_id");
        }
    }
}
