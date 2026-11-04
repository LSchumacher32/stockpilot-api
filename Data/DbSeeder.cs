using StockPilot.Models;

namespace StockPilot.Data;

public static class DbSeeder
{
    public static void Seed(StockPilotContext db)
    {
        if (db.Products.Any()) return;

        var electronics = new Category { Name = "Electronics" };
        var office = new Category { Name = "Office" };
        var gaming = new Category { Name = "Gaming" };
        var smartHome = new Category { Name = "SmartHome" };

        var technova = new Supplier { Name = "TechNova GmbH", Country = "DE", ContactEmail = "sales@technova.example" };
        var cableWorks = new Supplier { Name = "CableWorks BV", Country = "NL", ContactEmail = "info@cableworks.example" };
        var buroPlus = new Supplier { Name = "BüroPlus GmbH", Country = "DE", ContactEmail = "service@buroplus.example" };
        var playZone = new Supplier { Name = "PlayZone Ltd", Country = "GB", ContactEmail = "trade@playzone.example" };
        var homeSense = new Supplier { Name = "HomeSense AB", Country = "SE", ContactEmail = "b2b@homesense.example" };

        db.Categories.AddRange(electronics, office, gaming, smartHome);
        db.Suppliers.AddRange(technova, cableWorks, buroPlus, playZone, homeSense);

        Product P(string name, Category c, Supplier s, decimal price, int stock) => new()
        {
            ProductName = name,
            Category = c,
            Supplier = s,
            Price = price,
            StockLevel = stock
        };

        db.Products.AddRange(
            // Electronics
            P("Bluetooth Maus", electronics, technova, 24.90m, 8),
            P("Mechanische Tastatur", electronics, technova, 79.99m, 35),
            P("USB-C Hub 7-in-1", electronics, cableWorks, 39.90m, 52),
            P("27-Zoll Monitor", electronics, technova, 189.00m, 6),
            P("Webcam Full HD", electronics, technova, 49.50m, 22),
            P("Externe SSD 1 TB", electronics, technova, 94.90m, 14),
            P("HDMI-Kabel 2 m", electronics, cableWorks, 7.99m, 140),

            // Office
            P("Druckerpapier A4 500 Blatt", office, buroPlus, 5.49m, 300),
            P("Kugelschreiber 10er Set", office, buroPlus, 3.99m, 180),
            P("Ordner A4 breit", office, buroPlus, 2.79m, 95),
            P("Tacker mit Heftklammern", office, buroPlus, 8.90m, 4),
            P("Haftnotizen Block", office, buroPlus, 1.99m, 210),
            P("Bürostuhl ergonomisch", office, buroPlus, 249.00m, 9),
            P("Schreibtischlampe LED", office, homeSense, 29.90m, 27),

            // Gaming
            P("Gaming Headset 7.1", gaming, playZone, 59.90m, 31),
            P("Gaming Controller Wireless", gaming, playZone, 44.99m, 18),
            P("XXL Mauspad", gaming, playZone, 14.90m, 75),
            P("Gaming Stuhl Pro", gaming, playZone, 279.00m, 5),
            P("RGB Tastatur 60 %", gaming, technova, 69.00m, 12),
            P("Capture Card 4K", gaming, technova, 119.00m, 7),

            // SmartHome
            P("Smarte Steckdose WLAN", smartHome, homeSense, 12.99m, 160),
            P("LED-Lampe E27 RGB", smartHome, homeSense, 9.49m, 88),
            P("Smarter Thermostat", smartHome, homeSense, 64.90m, 15),
            P("Überwachungskamera Innen", smartHome, homeSense, 34.99m, 9),
            P("Tür- und Fenstersensor", smartHome, homeSense, 17.90m, 42),
            P("Smart Speaker mit Display", smartHome, homeSense, 89.00m, 3)
        );

        db.SaveChanges();
    }
}