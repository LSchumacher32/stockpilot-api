using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StockPilot.Data;
using StockPilot.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StockPilotContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")
                      ?? "Data Source=stockpilot.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StockPilotContext>();
    db.Database.Migrate();
    DbSeeder.Seed(db);
}

app.UseExceptionHandler(errorApp => errorApp.Run(async ctx =>
{
    ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await ctx.Response.WriteAsJsonAsync(new { message = "Unexpected server error." });
}));

app.Use(async (ctx, next) =>
{
    var sw = Stopwatch.StartNew();
    await next();
    sw.Stop();
    app.Logger.LogInformation("{Method} {Path} -> {Status} in {Ms} ms",
        ctx.Request.Method, ctx.Request.Path, ctx.Response.StatusCode, sw.ElapsedMilliseconds);
});

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.UseHttpsRedirection();

app.MapGet("/", () => "StockPilot API läuft");
app.MapProductEndpoints();
app.MapLookupEndpoints();

app.Run();