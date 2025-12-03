using Microsoft.AspNetCore.Mvc;
using FoodStock.Reports.ReportsInventory.Application.QueryServices;
using FoodStock.Reports.ReportsInventory.Application.Queries;

namespace FoodStock.Reports.ReportsInventory.Interfaces.REST;

[ApiController]
[Route("api/v1/inventory-reports")]
public class InventoryReportController : ControllerBase
{
    private readonly InventoryReportQueryService _service;

    public InventoryReportController(InventoryReportQueryService service)
    {
        _service = service;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var result = await _service.Handle(new GetInventorySummaryQuery());
        return Ok(result);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var result = await _service.Handle(new GetInventoryProductsQuery());
        return Ok(result);
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems()
    {
        var result = await _service.Handle(new GetInventoryItemsQuery());
        return Ok(result);
    }
}