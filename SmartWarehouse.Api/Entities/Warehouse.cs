using SmartWarehouse.Api.Data;

namespace SmartWarehouse.Api.Entities;

public class Warehouse : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;
}

