using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class PromotionUserMap : IEntityTypeConfiguration<PromotionUser>
    {
        public void Configure(EntityTypeBuilder<PromotionUser> builder)
        {
            builder.ToTable("tb_promotion_user");

            builder.HasKey(e => e.Id)
                .HasName("pk_promotion_user");

            builder.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnType("uuid")
                .HasColumnName("promotion_user_id");

            builder.Property(e => e.PromotionId)
               .HasColumnName("promotion_id");

            builder.Property(e => e.UserId)
               .HasColumnName("user_id");

            builder.HasIndex(e => e.UserId)
               .HasDatabaseName("ix_promotion_user_user_id");

            builder.HasOne(x => x.Promotion);
            builder.HasOne(x => x.User);
        }
    }
}
