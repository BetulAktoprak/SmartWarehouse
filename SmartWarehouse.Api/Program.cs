using Microsoft.EntityFrameworkCore;
using SmartWarehouse.Api.Data;
using SmartWarehouse.Api.Managers;
using SmartWarehouse.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<StockMovementRepository>();
builder.Services.AddScoped<ProductManager>();
builder.Services.AddScoped<StockManager>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
