using Microsoft.EntityFrameworkCore;
using StockPilot.Models;

namespace StockPilot.Data
{
    public class StockPilotContext : DbContext
    {
        public StockPilotContext(DbContextOptions<StockPilotContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Category>(e =>
            {
                e.Property(c => c.Name).HasMaxLength(50).IsRequired();
                e.HasIndex(c => c.Name).IsUnique();
            });

            b.Entity<Supplier>(e =>
            {
                e.Property(s => s.Name).HasMaxLength(100).IsRequired();
                e.Property(s => s.Country).HasMaxLength(50).IsRequired();
            });

            b.Entity<Product>(e =>
            {
                e.Property(p => p.ProductName).HasMaxLength(100).IsRequired();
                e.Property(p => p.Price).HasConversion<double>();
                e.HasOne(p => p.Category).WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(p => p.Supplier).WithMany(s => s.Products)
                    .HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}