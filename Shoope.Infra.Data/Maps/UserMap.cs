using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tb_users");

            builder.HasKey(e => e.Id)
                .HasName("pk_user");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("user_id");

            builder.Property(e => e.Name)
                .HasColumnName("name");

            builder.Property(e => e.Email)
                .HasColumnName("email");

            //builder.HasIndex(e => e.Email)
            //    .IsUnique()
            //    .HasDatabaseName("uq_user_email");

            builder.Property(e => e.Gender)
                .HasColumnName("gender");

            builder.Property(e => e.Phone)
                .IsRequired(true)
                .HasColumnName("phone");

            builder.Property(e => e.PasswordHash)
                .IsRequired(true)
                .HasColumnName("password_hash");

            builder.Property(e => e.Salt)
                .IsRequired(true)
                .HasColumnName("salt");

            builder.Property(e => e.Cpf)
                .HasColumnName("cpf");

            builder.Property(e => e.BirthDate)
                .HasColumnName("birth_date");

            //builder.Property(e => e.Token)
            //    .HasColumnName("cpf");

            builder.Property(e => e.UserImage)
                .IsRequired(false)
                .HasColumnName("user_image");

            //builder.Ignore(e => e.Token);
            //builder.Ignore(e => e.ConfirmEmail);
        }
    }
}

//public Guid Id { get; private set; }
//public string? Name { get; private set; }
//public string? Email { get; private set; }
//public string? Gender { get; private set; }
//public string? PasswordHash { get; private set; }
//public string? Cpf { get; private set; }
//public DateTime? BirthDate { get; private set; }
//public string? Token { get; private set; }
