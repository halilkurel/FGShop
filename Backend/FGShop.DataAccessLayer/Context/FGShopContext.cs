using FGShop.DataAccessLayer.Configurations;
using FGShop.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FGShop.DataAccessLayer.Context
{
    public class FGShopContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {

        public FGShopContext(DbContextOptions<FGShopContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProducthasCategory> ProducthasCategories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProducthasImage> ProducthasImages { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProducthasColor> ProducthasColors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProducthasColorAndSize> producthasColorAndSizes { get; set; }
        public DbSet<ProducthasColorAndSizehasStock> producthasColorAndSizehasStocks { get; set; }

    }
}
