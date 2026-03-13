using SmartWarehouse.Api.Data;

namespace SmartWarehouse.Api.Entities;

public class StockMovement : BaseEntity
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public MovementType MovementType { get; set; }

    public int ShelfId { get; set; }

    public DateTime MovementDate { get; set; }
}

