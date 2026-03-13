namespace SmartWarehouse.Api.DTOs;

public class DashboardSummaryDto
{
    public int TotalProducts { get; set; }

    public int TodayIn { get; set; }

    public int TodayOut { get; set; }

    public int InStock { get; set; }
}
