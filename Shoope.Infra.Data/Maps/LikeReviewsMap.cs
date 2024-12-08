using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class LikeReviewsMap : IEntityTypeConfiguration<LikeReview>
    {
        public void Configure(EntityTypeBuilder<LikeReview> builder)
        {
            builder.ToTable("tb_like_reviews");

            builder.HasKey(e => e.Id)
                .HasName("pk_like_reviews");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("like_reviews_id");

            builder.Property(e => e.ProductFlashSaleReviewsId)
                .IsRequired(true)
               .HasColumnName("product_flash_sale_reviews_id");

            builder.Property(e => e.UserId)
                .IsRequired(true)
               .HasColumnName("user_id");
        }
    }
}
