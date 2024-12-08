using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class UserSellerProductMap : IEntityTypeConfiguration<UserSellerProduct>
    {
        public void Configure(EntityTypeBuilder<UserSellerProduct> builder)
        {
            builder.ToTable("tb_user_seller_products");

            builder.HasKey(e => e.Id)
                .HasName("pk_user_seller_products");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("user_seller_products_id");

            builder.Property(e => e.Name)
                .IsRequired(true)
               .HasColumnName("name");

            builder.Property(e => e.ImgPerfil)
                .IsRequired(true)
               .HasColumnName("img_perfil");

            builder.Property(e => e.ImgPerfilPublicId)
                .IsRequired(true)
               .HasColumnName("img_perfil_public_id");

            builder.Property(e => e.ImgFloating)
                .IsRequired(false)
               .HasColumnName("img_floating");

            builder.Property(e => e.ImgFloatingPublicId)
                .IsRequired(false)
               .HasColumnName("img_floating_public_id");

            builder.Property(e => e.LastLogin)
                .IsRequired(false)
               .HasColumnName("last_login");

            builder.Property(e => e.Reviews)
                .IsRequired(false)
               .HasColumnName("reviews");

            builder.Property(e => e.ChatResponseRate)
                .IsRequired(false)
               .HasColumnName("chat_response_rate");

            builder.Property(e => e.AccountCreationDate)
                .IsRequired(true)
               .HasColumnName("account_creation_date");

            builder.Property(e => e.QuantityOfProductSold)
               .IsRequired(false)
              .HasColumnName("quantity_of_product_sold");

            builder.Property(e => e.UsuallyRespondsToChatIn)
               .IsRequired(false)
              .HasColumnName("usually_responds_to_chat_in");

            builder.Property(e => e.Followers)
               .IsRequired(false)
              .HasColumnName("followers");
        }
    }
}