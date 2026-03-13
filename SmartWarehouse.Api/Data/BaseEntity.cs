using System;

namespace SmartWarehouse.Api.Data;

public class BaseEntity
{
    public int Id { get; set; }
    public string CompanyId { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

