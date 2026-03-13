using SmartWarehouse.Api.DTOs;
using SmartWarehouse.Api.Entities;
using SmartWarehouse.Api.Repositories;

namespace SmartWarehouse.Api.Managers;

public class StockManager
{
    private readonly StockMovementRepository _stockMovementRepository;

    public StockManager(StockMovementRepository stockMovementRepository)
    {
        _stockMovementRepository = stockMovementRepository;
    }

    public async Task CreateMovementAsync(CreateStockMovementDto dto)
    {
        var entity = new StockMovement
        {
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            MovementType = dto.MovementType,
            ShelfId = dto.ShelfId,
            CompanyId = dto.CompanyId,
            MovementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        await _stockMovementRepository.AddAsync(entity);
    }
}

