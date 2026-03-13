using Microsoft.EntityFrameworkCore;
using SmartWarehouse.Api.Data;
using SmartWarehouse.Api.Entities;

namespace SmartWarehouse.Api.Repositories;

public class StockMovementRepository
{
    private readonly AppDbContext _context;

    public StockMovementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(StockMovement entity)
    {
        await _context.StockMovements.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<StockMovement>> GetByProductAsync(int productId, string companyId)
    {
        return await _context.StockMovements
            .Where(x =>
                x.ProductId == productId &&
                x.CompanyId == companyId &&
                !x.IsDeleted)
            .OrderByDescending(x => x.MovementDate)
            .ToListAsync();
    }
}

