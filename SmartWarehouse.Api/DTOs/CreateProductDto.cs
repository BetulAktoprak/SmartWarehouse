namespace SmartWarehouse.Api.DTOs;

public class CreateProductDto
{
    public string CompanyId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string Category { get; set; } = null!;
}

