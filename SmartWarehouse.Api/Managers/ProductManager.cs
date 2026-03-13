using Microsoft.EntityFrameworkCore;
using SmartWarehouse.Api.DTOs;
using SmartWarehouse.Api.Entities;
using SmartWarehouse.Api.Repositories;

namespace SmartWarehouse.Api.Managers;

public class ProductManager
{
    private readonly ProductRepository _repository;

    public ProductManager(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(CreateProductDto dto)
    {
        var entity = new Product
        {
            ProductName = dto.ProductName,
            Barcode = dto.Barcode,
            Category = dto.Category,
            CompanyId = dto.CompanyId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
    }

    public async Task<Product?> GetByIdAsync(int id, string companyId)
    {
        return await _repository.GetByIdAsync(id, companyId);
    }

    public async Task<bool> UpdateAsync(UpdateProductDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id, dto.CompanyId);
        if (entity == null)
        {
            return false;
        }

        entity.ProductName = dto.ProductName;
        entity.Barcode = dto.Barcode;
        entity.Category = dto.Category;
        entity.UpdatedAt = DateTime.UtcNow;

        _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SoftDeleteAsync(DeleteProductDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id, dto.CompanyId);
        if (entity == null)
        {
            return false;
        }

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;

        _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<(IReadOnlyList<Product> Data, int TotalCount)> ListAsync(
        string companyId,
        int page,
        int pageSize,
        string? search)
    {
        return await _repository.GetPagedAsync(companyId, page, pageSize, search);
    }
}

