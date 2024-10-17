using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;

namespace Shoope.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            //Users = Set<User>();
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Address> Addresses { get; private set; }
        public DbSet<Promotion> Promotion { get; private set; }
        public DbSet<PromotionUser> PromotionUser { get; private set; }
        public DbSet<Cupon> Cupons { get; private set; }
        public DbSet<UserCupon> UserCupons { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
