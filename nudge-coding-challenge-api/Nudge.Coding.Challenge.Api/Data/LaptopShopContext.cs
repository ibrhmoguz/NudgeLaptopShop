using Microsoft.EntityFrameworkCore;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Data
{
    public class LaptopShopContext: DbContext
    {
        public LaptopShopContext(DbContextOptions<LaptopShopContext> options) : base(options)
        {
        }

        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<LaptopConfiguration> LaptopConfigurations { get; set; }
        public DbSet<Basket> Basket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Laptop>().ToTable("Laptop");
            modelBuilder.Entity<LaptopConfiguration>().ToTable("LaptopConfiguration");
            modelBuilder.Entity<Basket>().ToTable("Basket").HasKey(b => new {b.LaptopId, b.LaptopConfigurationId});
        }
    }
}
