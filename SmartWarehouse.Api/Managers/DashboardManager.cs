using SmartWarehouse.Api.DTOs;
using SmartWarehouse.Api.Repositories;

namespace SmartWarehouse.Api.Managers;

public class DashboardManager
{
    private readonly StockMovementRepository _stockRepository;
    private readonly ProductRepository _productRepository;

    public DashboardManager(
        StockMovementRepository stockRepository,
        ProductRepository productRepository)
    {
        _stockRepository = stockRepository;
        _productRepository = productRepository;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(string companyId)
    {
        var todayIn = await _stockRepository.GetTodayInAsync(companyId);
        var todayOut = await _stockRepository.GetTodayOutAsync(companyId);
        var inStock = await _stockRepository.GetTotalStockAsync(companyId);
        //var totalProducts = await _productRepository.CountAsync(companyId);

        return new DashboardSummaryDto
        {
            TodayIn = todayIn,
            TodayOut = todayOut,
            InStock = inStock,
            //TotalProducts = totalProducts
        };
    }
}
