using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class PromotionMap : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("tb_promotion");

            builder.HasKey(e => e.Id)
                .HasName("pk_promotion");

            builder.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnType("uuid")
                .HasColumnName("promotion_id");

            builder.Property(e => e.WhatIsThePromotion)
               .HasColumnName("what_is_the_promotion");

            builder.Property(e => e.Title)
               .HasColumnName("title");

            builder.Property(e => e.Description)
               .HasColumnName("description");

            builder.Property(e => e.Date)
                .HasColumnName("date");

            builder.Property(e => e.Img)
               .HasColumnName("img");

            builder.Property(e => e.PublicIdImg)
                .IsRequired(true)
               .HasColumnName("public_id_img");

            builder.Property(e => e.ImgInnerFirst)
                .IsRequired(false)
               .HasColumnName("img_inner_first");

            builder.Property(e => e.AltImgInnerFirst)
                .IsRequired(false)
               .HasColumnName("alt_img_inner_first");

            builder.Property(e => e.ImgInnerSecond)
                .IsRequired(false)
               .HasColumnName("img_inner_second");

            builder.Property(e => e.AltImgInnerSecond)
                .IsRequired(false)
               .HasColumnName("alt_img_inner_second");

            builder.Property(e => e.ImgInnerThird)
                .IsRequired(false)
               .HasColumnName("img_inner_third");

            builder.Property(e => e.AltImgInnerThird)
                .IsRequired(false)
               .HasColumnName("alt_img_inner_third");

            builder.Property(e => e.ImgInnerFirstPublicId)
                .IsRequired(false)
               .HasColumnName("img_inner_first_public_id");

            builder.Property(e => e.ImgInnerSecondPublicId)
                .IsRequired(false)
               .HasColumnName("img_inner_second_public_id");

            builder.Property(e => e.ImgInnerThirdPublicId)
                .IsRequired(false)
               .HasColumnName("img_inner_third_public-id");
        }
    }
}