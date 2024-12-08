using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class CuponMap : IEntityTypeConfiguration<Cupon>
    {
        public void Configure(EntityTypeBuilder<Cupon> builder)
        {
            builder.ToTable("tb_cupons");

            builder.HasKey(e => e.Id)
                .HasName("pk_cupons");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("cupons_id");

            builder.Property(e => e.FirstText)
                .IsRequired()
               .HasColumnName("first_text");

            builder.Property(e => e.SecondText)
                .IsRequired()
               .HasColumnName("second_text");

            builder.Property(e => e.ThirdText)
                .IsRequired()
               .HasColumnName("third_text");

            builder.Property(e => e.DateValidateCupon)
                .IsRequired()
               .HasColumnName("date_validate_cupon");

            builder.Property(e => e.QuantityCupons)
                .IsRequired()
               .HasColumnName("quantity_cupons");

            builder.Property(e => e.WhatCuponNumber)
                .IsRequired()
               .HasColumnName("what_cupon_number");

            builder.Property(e => e.SecondImg)
                .IsRequired(false)
               .HasColumnName("second_img");

            builder.Property(e => e.SecondImgAlt)
                .IsRequired(false)
               .HasColumnName("second_img_alt");
        }
    }
}
