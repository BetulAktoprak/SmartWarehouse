namespace SmartWarehouse.Api.DTOs;

public class CreateStockMovementDto
{
    public string CompanyId { get; set; } = null!;

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    // "IN" veya "OUT"
    public string MovementType { get; set; } = null!;

    public int ShelfId { get; set; }
}

