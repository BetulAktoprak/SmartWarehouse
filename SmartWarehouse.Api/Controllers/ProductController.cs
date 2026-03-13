using Microsoft.AspNetCore.Mvc;
using SmartWarehouse.Api.DTOs;
using SmartWarehouse.Api.Managers;

namespace SmartWarehouse.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductManager _productManager;

    public ProductController(ProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CompanyId))
        {
            return BadRequest("CompanyId is required.");
        }

        await _productManager.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CompanyId))
        {
            return BadRequest("CompanyId is required.");
        }

        var success = await _productManager.UpdateAsync(dto);
        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CompanyId))
        {
            return BadRequest("CompanyId is required.");
        }

        var success = await _productManager.SoftDeleteAsync(dto);
        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet("by-company/{companyId}")]
    public async Task<IActionResult> GetByCompany(
        string companyId,
        int page = 1,
        int pageSize = 25,
        string? search = "")
    {
        if (string.IsNullOrWhiteSpace(companyId))
        {
            return BadRequest("CompanyId is required.");
        }

        if (page <= 0)
        {
            page = 1;
        }

        if (pageSize <= 0)
        {
            pageSize = 25;
        }

        var (data, totalCount) = await _productManager.ListAsync(companyId, page, pageSize, search);

        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return Ok(new
        {
            data,
            totalCount,
            page,
            pageSize,
            totalPages
        });
    }
}

