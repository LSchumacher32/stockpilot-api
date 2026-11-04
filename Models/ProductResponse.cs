namespace StockPilot.Models;

public record ProductResponse(
    int Id,
    string ProductName,
    string Category,
    string Supplier,
    decimal Price,
    int StockLevel);