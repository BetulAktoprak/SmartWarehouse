using SmartWarehouse.Api.Data;

namespace SmartWarehouse.Api.DTOs;

public class CreateStockMovementDto
{
    public string CompanyId { get; set; } = null!;

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    // "IN" veya "OUT"
    public MovementType MovementType { get; set; }

    public int ShelfId { get; set; }
}

