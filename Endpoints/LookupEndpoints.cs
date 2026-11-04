using Microsoft.EntityFrameworkCore;
using StockPilot.Data;

namespace StockPilot.Endpoints;

public static class LookupEndpoints
{
    public static void MapLookupEndpoints(this WebApplication app)
    {
        app.MapGet("/categories", async (StockPilotContext db) =>
            Results.Ok(await db.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync()))
            .WithTags("Lookups");

        app.MapGet("/suppliers", async (StockPilotContext db) =>
            Results.Ok(await db.Suppliers
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name, s.Country, s.ContactEmail })
                .ToListAsync()))
            .WithTags("Lookups");
    }
}