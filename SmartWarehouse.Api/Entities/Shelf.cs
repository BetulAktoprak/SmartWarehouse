using SmartWarehouse.Api.Data;

namespace SmartWarehouse.Api.Entities;

public class Shelf : BaseEntity
{
    public int WarehouseId { get; set; }

    public string Code { get; set; } = null!;

    public int Capacity { get; set; }
}

