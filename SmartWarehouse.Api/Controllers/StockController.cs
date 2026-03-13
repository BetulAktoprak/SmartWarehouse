using Microsoft.AspNetCore.Mvc;
using SmartWarehouse.Api.DTOs;
using SmartWarehouse.Api.Managers;

namespace SmartWarehouse.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly StockManager _stockManager;

    public StockController(StockManager stockManager)
    {
        _stockManager = stockManager;
    }

    [HttpPost("movement")]
    public async Task<IActionResult> CreateMovement(CreateStockMovementDto dto)
    {
        await _stockManager.CreateMovementAsync(dto);

        return Ok();
    }
}
