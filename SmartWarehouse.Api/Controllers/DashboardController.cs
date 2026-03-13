using Microsoft.AspNetCore.Mvc;
using SmartWarehouse.Api.Managers;

namespace SmartWarehouse.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly DashboardManager _dashboardManager;

    public DashboardController(DashboardManager dashboardManager)
    {
        _dashboardManager = dashboardManager;
    }

    [HttpGet("summary/{companyId}")]
    public async Task<IActionResult> GetSummary(string companyId)
    {
        var result = await _dashboardManager.GetSummaryAsync(companyId);

        return Ok(result);
    }
}
