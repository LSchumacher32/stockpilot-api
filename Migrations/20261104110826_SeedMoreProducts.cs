using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockPilot.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                table: "Products",
                newName: "StockLevel");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Price", "ProductName", "StockLevel" },
                values: new object[,]
                {
                    { 1, "Electronics", 24.90m, "Bluetooth Maus", 8 },
                    { 2, "Electronics", 79.99m, "Mechanische Tastatur", 35 },
                    { 3, "Electronics", 39.90m, "USB-C Hub 7-in-1", 52 },
                    { 4, "Electronics", 189.00m, "27-Zoll Monitor", 6 },
                    { 5, "Electronics", 49.50m, "Webcam Full HD", 22 },
                    { 6, "Electronics", 94.90m, "Externe SSD 1 TB", 14 },
                    { 7, "Electronics", 7.99m, "HDMI-Kabel 2 m", 140 },
                    { 8, "Office", 5.49m, "Druckerpapier A4 500 Blatt", 300 },
                    { 9, "Office", 3.99m, "Kugelschreiber 10er Set", 180 },
                    { 10, "Office", 2.79m, "Ordner A4 breit", 95 },
                    { 11, "Office", 8.90m, "Tacker mit Heftklammern", 4 },
                    { 12, "Office", 1.99m, "Haftnotizen Block", 210 },
                    { 13, "Office", 249.00m, "Bürostuhl ergonomisch", 9 },
                    { 14, "Office", 29.90m, "Schreibtischlampe LED", 27 },
                    { 15, "Gaming", 59.90m, "Gaming Headset 7.1", 31 },
                    { 16, "Gaming", 44.99m, "Gaming Controller Wireless", 18 },
                    { 17, "Gaming", 14.90m, "XXL Mauspad", 75 },
                    { 18, "Gaming", 279.00m, "Gaming Stuhl Pro", 5 },
                    { 19, "Gaming", 69.00m, "RGB Tastatur 60 %", 12 },
                    { 20, "Gaming", 119.00m, "Capture Card 4K", 7 },
                    { 21, "SmartHome", 12.99m, "Smarte Steckdose WLAN", 160 },
                    { 22, "SmartHome", 9.49m, "LED-Lampe E27 RGB", 88 },
                    { 23, "SmartHome", 64.90m, "Smarter Thermostat", 15 },
                    { 24, "SmartHome", 34.99m, "Überwachungskamera Innen", 9 },
                    { 25, "SmartHome", 17.90m, "Tür- und Fenstersensor", 42 },
                    { 26, "SmartHome", 89.00m, "Smart Speaker mit Display", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.RenameColumn(
                name: "StockLevel",
                table: "Products",
                newName: "StockQuantity");
        }
    }
}
