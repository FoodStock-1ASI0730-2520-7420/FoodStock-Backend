using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Reports.ReportsInventory.Application.Queries;
using FoodStock.Reports.ReportsInventory.Domain.Model;

namespace FoodStock.Reports.ReportsInventory.Application.QueryServices;

public class InventoryReportQueryService
{
    private readonly IProductRepository _productRepo;
    private readonly IDishRepository _dishRepo;

    public InventoryReportQueryService(
        IProductRepository productRepo,
        IDishRepository dishRepo)
    {
        _productRepo = productRepo;
        _dishRepo = dishRepo;
    }

    public async Task<InventorySummary> Handle(GetInventorySummaryQuery query)
    {
        var products = await _productRepo.ListAsync();

        var summary = new InventorySummary
        {
            TotalProducts = products.Count,
            TotalQuantity = products.Sum(p => p.Quantity),
            TotalValue = products.Sum(p => p.UnitPrice * p.Quantity)
        };

        return summary;
    }

    public async Task<IEnumerable<InventoryProductDto>> Handle(GetInventoryProductsQuery query)
    {
        var products = await _productRepo.ListAsync();

        return products.Select(p => new InventoryProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Category = p.Category,
            Quantity = p.Quantity,
            ExpirationDate = p.ExpirationDate,
            UnitPrice = p.UnitPrice,
            TotalValue = p.UnitPrice * p.Quantity
        });
    }

    public async Task<IEnumerable<InventoryItemDto>> Handle(GetInventoryItemsQuery query)
    {
        var dishes = await _dishRepo.ListAsync();

        return dishes.Select(d => new InventoryItemDto
        {
            Id = d.Id,
            Name = d.Name,
            Price = d.PriceUnit
        });
    }
}