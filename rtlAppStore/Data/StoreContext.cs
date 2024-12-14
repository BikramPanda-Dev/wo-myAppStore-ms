using Microsoft.EntityFrameworkCore;
using rtlAppStore.Data.config;
using rtlAppStore.Entities;

namespace rtlAppStore.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }

        public DbSet<Product> Products { get; set; }
    }
}
