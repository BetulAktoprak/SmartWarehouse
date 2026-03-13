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

    public async Task<int> GetTodayInAsync(string companyId)
    {
        var today = DateTime.UtcNow.Date;

        return await _context.StockMovements
            .Where(x =>
                x.CompanyId == companyId &&
                x.MovementType == MovementType.IN &&
                x.MovementDate.Date == today &&
                !x.IsDeleted)
            .SumAsync(x => x.Quantity);
    }

    public async Task<int> GetTodayOutAsync(string companyId)
    {
        var today = DateTime.UtcNow.Date;

        return await _context.StockMovements
            .Where(x =>
                x.CompanyId == companyId &&
                x.MovementType == MovementType.OUT &&
                x.MovementDate.Date == today &&
                !x.IsDeleted)
            .SumAsync(x => x.Quantity);
    }
    public async Task<int> GetTotalStockAsync(string companyId)
    {
        var totalIn = await _context.StockMovements
            .Where(x =>
                x.CompanyId == companyId &&
                x.MovementType == MovementType.IN &&
                !x.IsDeleted)
            .SumAsync(x => x.Quantity);

        var totalOut = await _context.StockMovements
            .Where(x =>
                x.CompanyId == companyId &&
                x.MovementType == MovementType.OUT &&
                !x.IsDeleted)
            .SumAsync(x => x.Quantity);

        return totalIn - totalOut;
    }

}

