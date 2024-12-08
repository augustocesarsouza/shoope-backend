using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class UserCuponMap : IEntityTypeConfiguration<UserCupon>
    {
        public void Configure(EntityTypeBuilder<UserCupon> builder)
        {
            builder.ToTable("tb_user_cupons");

            builder.HasKey(e => e.Id)
                .HasName("pk_user_cupons");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("user_cupons_id");

            builder.Property(e => e.CuponId)
               .HasColumnName("cupon_id");

            builder.Property(e => e.UserId)
               .HasColumnName("user_id");

            builder.HasIndex(e => e.UserId)
               .HasDatabaseName("ix_user_cupons_user_id");

            builder.HasOne(x => x.Cupon);
            builder.HasOne(x => x.User);
        }
    }
}