using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Maps
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("tb_address");

            builder.HasKey(e => e.Id)
                .HasName("pk_address");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("addres_id");

            builder.Property(e => e.FullName)
               .HasColumnName("full_name");

            builder.Property(e => e.PhoneNumber)
               .HasColumnName("phone_number");

            builder.Property(e => e.Cep)
               .HasColumnName("cep");

            builder.Property(e => e.StateCity)
               .HasColumnName("state_city");

            builder.Property(e => e.Neighborhood)
               .HasColumnName("neighborhood");

            builder.Property(e => e.Street)
               .HasColumnName("street");

            builder.Property(e => e.NumberHome)
               .HasColumnName("number_home");

            builder.Property(e => e.Complement)
                .IsRequired(false)
               .HasColumnName("complement");

            builder.Property(e => e.UserId)
               .HasColumnName("user_id");

            builder.Property(e => e.DefaultAddress)
                .IsRequired(true)
                .HasColumnType("smallint")
                .HasColumnName("default_address");

            builder.HasOne(x => x.User);
        }
    }
}
