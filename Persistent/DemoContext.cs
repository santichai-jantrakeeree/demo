using Core.Entities;
using Core.Entities.Identities;
using Core.Entities.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistent
{
    public class DemoContext: IdentityDbContext<AppUser, AppRole, int, 
        AppUserClaim, 
        AppUserRole, 
        AppUserLogin, 
        AppRoleClaim, 
        AppUserToken>
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }
        public DbSet<AppRole> AppRoles { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRoles");
            modelBuilder.Entity<AppRoleClaim>().ToTable("AppRoleClaims");
            modelBuilder.Entity<AppUserClaim>().ToTable("AppUserClaims");
            modelBuilder.Entity<AppUserToken>().ToTable("AppUserTokens");
            modelBuilder.Entity<AppUserLogin>().ToTable("AppUserLogins");
        }
    }
}