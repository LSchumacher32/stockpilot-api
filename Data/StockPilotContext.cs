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
    }
}