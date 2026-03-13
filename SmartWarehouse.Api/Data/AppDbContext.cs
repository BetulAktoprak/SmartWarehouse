using Microsoft.EntityFrameworkCore;
using SmartWarehouse.Api.Entities;

namespace SmartWarehouse.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Warehouse> Warehouses { get; set; } = null!;

    public DbSet<Shelf> Shelves { get; set; } = null!;

    public DbSet<StockMovement> StockMovements { get; set; } = null!;
}
