using System;
using SmartWarehouse.Api.Data;

namespace SmartWarehouse.Api.Entities;

public class StockMovement : BaseEntity
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string MovementType { get; set; } = null!;

    public int ShelfId { get; set; }

    public DateTime MovementDate { get; set; }
}

