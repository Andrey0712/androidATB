using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAtb.Data.Entities;
using WebAtb.Data.Entities.Identity;

namespace WebAtb.Data
{
    public class AppEFContext : IdentityDbContext<AppUser, AppRole, long, IdentityUserClaim<long>,
        AppUserRole, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options) :
            base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<UserProduct> UserProduct { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            base.OnModelCreating(builder);
            builder.Entity<UserProduct>(userProd =>
            {
                userProd.HasKey(tp => new { tp.UserId, tp.ProductId });

                userProd.HasOne(tp => tp.User)
                    .WithMany(t => t.UserProduct)
                    .HasForeignKey(tp => tp.UserId)
                    .IsRequired();

                userProd.HasOne(tp => tp.Product)
                    .WithMany(t => t.UserProduct)
                    .HasForeignKey(tp => tp.ProductId)
                    .IsRequired();
            });
        }
    }
}

