using Microsoft.EntityFrameworkCore;
using StockPilot.Data;
using StockPilot.Models;

namespace StockPilot.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        group.MapGet("/", async (StockPilotContext db, string? category, int? maxStock,
                                 string? sort, string? dir) =>
        {
            IQueryable<Product> query = db.Products.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);
            if (maxStock.HasValue)
                query = query.Where(p => p.StockLevel <= maxStock.Value);

            bool desc = string.Equals(dir, "desc", StringComparison.OrdinalIgnoreCase);
            query = sort?.ToLower() switch
            {
                "price" => desc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "stock" => desc ? query.OrderByDescending(p => p.StockLevel) : query.OrderBy(p => p.StockLevel),
                _ => query.OrderBy(p => p.ProductName)
            };
            return Results.Ok(await query.ToListAsync());
        });

        // Beispiel für sicheres Raw SQL: Interpolation wird zu SQL-Parametern
        group.MapGet("/search", async (StockPilotContext db, string name) =>
        {
            var pattern = $"%{name}%";
            var result = await db.Products
                .FromSqlInterpolated($"SELECT * FROM Products WHERE ProductName LIKE {pattern}")
                .AsNoTracking()
                .ToListAsync();
            return Results.Ok(result);
        });

        group.MapGet("/{id:int}", async (int id, StockPilotContext db) =>
            await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id)
                is Product p ? Results.Ok(p) : Results.NotFound(new { message = $"Product {id} not found" }));

        group.MapPost("/", async (ProductDto dto, StockPilotContext db) =>
        {
            var errors = Validate(dto);
            if (errors.Count > 0) return Results.ValidationProblem(errors);

            var product = new Product
            {
                ProductName = dto.ProductName.Trim(),
                Category = dto.Category.Trim(),
                Price = dto.Price,
                StockLevel = dto.StockLevel
            };
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/products/{product.Id}", product);
        });

        group.MapPut("/{id:int}", async (int id, ProductDto dto, StockPilotContext db) =>
        {
            var errors = Validate(dto);
            if (errors.Count > 0) return Results.ValidationProblem(errors);

            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound(new { message = $"Product {id} not found" });

            product.ProductName = dto.ProductName.Trim();
            product.Category = dto.Category.Trim();
            product.Price = dto.Price;
            product.StockLevel = dto.StockLevel;
            await db.SaveChangesAsync();
            return Results.Ok(product);
        });

        group.MapDelete("/{id:int}", async (int id, StockPilotContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound(new { message = $"Product {id} not found" });

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }

    private static Dictionary<string, string[]> Validate(ProductDto dto)
    {
        var errors = new Dictionary<string, string[]>();
        if (string.IsNullOrWhiteSpace(dto.ProductName) || dto.ProductName.Length > 100)
            errors["productName"] = new[] { "Name is required (max 100 characters)." };
        if (string.IsNullOrWhiteSpace(dto.Category) || dto.Category.Length > 50)
            errors["category"] = new[] { "Category is required (max 50 characters)." };
        if (dto.Price <= 0)
            errors["price"] = new[] { "Price must be greater than 0." };
        if (dto.StockLevel < 0)
            errors["stockLevel"] = new[] { "Stock level must not be negative." };
        return errors;
    }
}