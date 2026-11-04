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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
    // Electronics
        new Product { Id = 1, ProductName = "Bluetooth Maus", Category = "Electronics", Price = 24.90m, StockLevel = 8 },
        new Product { Id = 2, ProductName = "Mechanische Tastatur", Category = "Electronics", Price = 79.99m, StockLevel = 35 },
        new Product { Id = 3, ProductName = "USB-C Hub 7-in-1", Category = "Electronics", Price = 39.90m, StockLevel = 52 },
        new Product { Id = 4, ProductName = "27-Zoll Monitor", Category = "Electronics", Price = 189.00m, StockLevel = 6 },
        new Product { Id = 5, ProductName = "Webcam Full HD", Category = "Electronics", Price = 49.50m, StockLevel = 22 },
        new Product { Id = 6, ProductName = "Externe SSD 1 TB", Category = "Electronics", Price = 94.90m, StockLevel = 14 },
        new Product { Id = 7, ProductName = "HDMI-Kabel 2 m", Category = "Electronics", Price = 7.99m, StockLevel = 140 },
    // Office
        new Product { Id = 8, ProductName = "Druckerpapier A4 500 Blatt", Category = "Office", Price = 5.49m, StockLevel = 300 },
        new Product { Id = 9, ProductName = "Kugelschreiber 10er Set", Category = "Office", Price = 3.99m, StockLevel = 180 },
        new Product { Id = 10, ProductName = "Ordner A4 breit", Category = "Office", Price = 2.79m, StockLevel = 95 },
        new Product { Id = 11, ProductName = "Tacker mit Heftklammern", Category = "Office", Price = 8.90m, StockLevel = 4 },
        new Product { Id = 12, ProductName = "Haftnotizen Block", Category = "Office", Price = 1.99m, StockLevel = 210 },
        new Product { Id = 13, ProductName = "Bürostuhl ergonomisch", Category = "Office", Price = 249.00m, StockLevel = 9 },
        new Product { Id = 14, ProductName = "Schreibtischlampe LED", Category = "Office", Price = 29.90m, StockLevel = 27 },
    // Gaming
        new Product { Id = 15, ProductName = "Gaming Headset 7.1", Category = "Gaming", Price = 59.90m, StockLevel = 31 },
        new Product { Id = 16, ProductName = "Gaming Controller Wireless", Category = "Gaming", Price = 44.99m, StockLevel = 18 },
        new Product { Id = 17, ProductName = "XXL Mauspad", Category = "Gaming", Price = 14.90m, StockLevel = 75 },
        new Product { Id = 18, ProductName = "Gaming Stuhl Pro", Category = "Gaming", Price = 279.00m, StockLevel = 5 },
        new Product { Id = 19, ProductName = "RGB Tastatur 60 %", Category = "Gaming", Price = 69.00m, StockLevel = 12 },
        new Product { Id = 20, ProductName = "Capture Card 4K", Category = "Gaming", Price = 119.00m, StockLevel = 7 },
    // SmartHome
        new Product { Id = 21, ProductName = "Smarte Steckdose WLAN", Category = "SmartHome", Price = 12.99m, StockLevel = 160 },
        new Product { Id = 22, ProductName = "LED-Lampe E27 RGB", Category = "SmartHome", Price = 9.49m, StockLevel = 88 },
        new Product { Id = 23, ProductName = "Smarter Thermostat", Category = "SmartHome", Price = 64.90m, StockLevel = 15 },
        new Product { Id = 24, ProductName = "Überwachungskamera Innen", Category = "SmartHome", Price = 34.99m, StockLevel = 9 },
        new Product { Id = 25, ProductName = "Tür- und Fenstersensor", Category = "SmartHome", Price = 17.90m, StockLevel = 42 },
        new Product { Id = 26, ProductName = "Smart Speaker mit Display", Category = "SmartHome", Price = 89.00m, StockLevel = 3 }

            );
        }
    }
}