using Microsoft.EntityFrameworkCore;
using StockPilot.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StockPilotContext>(options =>
    options.UseSqlite("Data Source=stockpilot.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "StockPilot API läuft");

app.Run();