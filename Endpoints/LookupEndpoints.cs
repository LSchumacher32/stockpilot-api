using Microsoft.EntityFrameworkCore;
using StockPilot.Data;
using StockPilot.Models;

namespace StockPilot.Endpoints;

public record CategoryDto(string Name);
public record SupplierDto(string Name, string Country, string? ContactEmail);

public static class LookupEndpoints
{
    public static void MapLookupEndpoints(this WebApplication app)
    {
        MapCategories(app);
        MapSuppliers(app);
    }

    private static void MapCategories(WebApplication app)
    {
        var g = app.MapGroup("/categories").WithTags("Categories");

        g.MapGet("/", async (StockPilotContext db) =>
            Results.Ok(await db.Categories.AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync()));

        g.MapGet("/{id:int}", async (int id, StockPilotContext db) =>
            await db.Categories.AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new { c.Id, c.Name })
                .FirstOrDefaultAsync()
                is { } c ? Results.Ok(c) : Results.NotFound(new { message = $"Category {id} not found" }));

        g.MapPost("/", async (CategoryDto dto, StockPilotContext db) =>
        {
            var name = dto.Name?.Trim() ?? "";
            if (name.Length == 0 || name.Length > 50)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                { ["name"] = new[] { "Name is required (max 50 characters)." } });
            if (await db.Categories.AnyAsync(c => c.Name == name))
                return Results.Conflict(new { message = $"Category '{name}' already exists" });

            var cat = new Category { Name = name };
            db.Categories.Add(cat);
            await db.SaveChangesAsync();
            return Results.Created($"/categories/{cat.Id}", new { cat.Id, cat.Name });
        });

        g.MapPut("/{id:int}", async (int id, CategoryDto dto, StockPilotContext db) =>
        {
            var name = dto.Name?.Trim() ?? "";
            if (name.Length == 0 || name.Length > 50)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                { ["name"] = new[] { "Name is required (max 50 characters)." } });

            var cat = await db.Categories.FindAsync(id);
            if (cat is null) return Results.NotFound(new { message = $"Category {id} not found" });
            if (await db.Categories.AnyAsync(c => c.Name == name && c.Id != id))
                return Results.Conflict(new { message = $"Category '{name}' already exists" });

            cat.Name = name;
            await db.SaveChangesAsync();
            return Results.Ok(new { cat.Id, cat.Name });
        });

        g.MapDelete("/{id:int}", async (int id, StockPilotContext db) =>
        {
            var cat = await db.Categories.FindAsync(id);
            if (cat is null) return Results.NotFound(new { message = $"Category {id} not found" });
            if (await db.Products.AnyAsync(p => p.CategoryId == id))
                return Results.Conflict(new { message = "Category is still used by products" });

            db.Categories.Remove(cat);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        g.MapDelete("/", async (StockPilotContext db) =>
        {
            if (await db.Products.AnyAsync())
                return Results.Conflict(new { message = "Delete all products first" });
            var count = await db.Categories.ExecuteDeleteAsync();
            return Results.Ok(new { deleted = count });
        });
    }

    private static void MapSuppliers(WebApplication app)
    {
        var g = app.MapGroup("/suppliers").WithTags("Suppliers");

        g.MapGet("/", async (StockPilotContext db) =>
            Results.Ok(await db.Suppliers.AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name, s.Country, s.ContactEmail })
                .ToListAsync()));

        g.MapGet("/{id:int}", async (int id, StockPilotContext db) =>
            await db.Suppliers.AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new { s.Id, s.Name, s.Country, s.ContactEmail })
                .FirstOrDefaultAsync()
                is { } s ? Results.Ok(s) : Results.NotFound(new { message = $"Supplier {id} not found" }));

        g.MapPost("/", async (SupplierDto dto, StockPilotContext db) =>
        {
            var errors = Validate(dto);
            if (errors.Count > 0) return Results.ValidationProblem(errors);

            var sup = new Supplier
            {
                Name = dto.Name.Trim(),
                Country = dto.Country.Trim(),
                ContactEmail = dto.ContactEmail?.Trim()
            };
            db.Suppliers.Add(sup);
            await db.SaveChangesAsync();
            return Results.Created($"/suppliers/{sup.Id}",
                new { sup.Id, sup.Name, sup.Country, sup.ContactEmail });
        });

        g.MapPut("/{id:int}", async (int id, SupplierDto dto, StockPilotContext db) =>
        {
            var errors = Validate(dto);
            if (errors.Count > 0) return Results.ValidationProblem(errors);

            var sup = await db.Suppliers.FindAsync(id);
            if (sup is null) return Results.NotFound(new { message = $"Supplier {id} not found" });

            sup.Name = dto.Name.Trim();
            sup.Country = dto.Country.Trim();
            sup.ContactEmail = dto.ContactEmail?.Trim();
            await db.SaveChangesAsync();
            return Results.Ok(new { sup.Id, sup.Name, sup.Country, sup.ContactEmail });
        });

        g.MapDelete("/{id:int}", async (int id, StockPilotContext db) =>
        {
            var sup = await db.Suppliers.FindAsync(id);
            if (sup is null) return Results.NotFound(new { message = $"Supplier {id} not found" });
            if (await db.Products.AnyAsync(p => p.SupplierId == id))
                return Results.Conflict(new { message = "Supplier is still used by products" });

            db.Suppliers.Remove(sup);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        g.MapDelete("/", async (StockPilotContext db) =>
        {
            if (await db.Products.AnyAsync())
                return Results.Conflict(new { message = "Delete all products first" });
            var count = await db.Suppliers.ExecuteDeleteAsync();
            return Results.Ok(new { deleted = count });
        });
    }

    private static Dictionary<string, string[]> Validate(SupplierDto dto)
    {
        var errors = new Dictionary<string, string[]>();
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 100)
            errors["name"] = new[] { "Name is required (max 100 characters)." };
        if (string.IsNullOrWhiteSpace(dto.Country) || dto.Country.Length > 50)
            errors["country"] = new[] { "Country is required (max 50 characters)." };
        if (!string.IsNullOrWhiteSpace(dto.ContactEmail) && !dto.ContactEmail.Contains('@'))
            errors["contactEmail"] = new[] { "Email is not valid." };
        return errors;
    }
}