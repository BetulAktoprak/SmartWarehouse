using System.Collections.Generic;
using SmartWarehouse.Api.Data;

namespace SmartWarehouse.Api.Entities;

public class Product : BaseEntity
{
    public string ProductName { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string Category { get; set; } = null!;

    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}

