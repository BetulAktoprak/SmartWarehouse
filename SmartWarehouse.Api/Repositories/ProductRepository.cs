using Microsoft.EntityFrameworkCore;
using SmartWarehouse.Api.Data;
using SmartWarehouse.Api.Entities;

namespace SmartWarehouse.Api.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(int id, string companyId)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.CompanyId == companyId &&
                !x.IsDeleted);
    }

    public async Task<(IReadOnlyList<Product> Data, int TotalCount)> GetPagedAsync(
        string companyId,
        int page,
        int pageSize,
        string? search)
    {
        var query = _context.Products
            .Where(x => x.CompanyId == companyId && !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.ProductName.Contains(search));
        }

        var totalCount = await query.CountAsync();

        var data = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, totalCount);
    }

    public void Update(Product entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

