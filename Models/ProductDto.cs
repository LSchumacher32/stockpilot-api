namespace StockPilot.Models;

public record ProductDto(
    string ProductName,
    int CategoryId,
    int SupplierId,
    decimal Price,
    int StockLevel);