using FoodStock.Reports.SalesReports.Application.Queries;
using FoodStock.Reports.SalesReports.Application.QueryServices;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Reports.SalesReports.Interfaces.REST;

[ApiController]
[Route("api/v1/reports/sales")]
public class SalesReportController(
    SalesReportQueryService reportQueryService) : ControllerBase
{
    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview()
        => Ok(await reportQueryService.Handle(new GetSalesReportQuery()));

    [HttpGet("payments")]
    public async Task<IActionResult> GetPaymentSummary()
        => Ok(await reportQueryService.Handle(new GetPaymentSummaryQuery()));

    [HttpGet("best-dishes")]
    public async Task<IActionResult> GetBestSellingDishes()
        => Ok(await reportQueryService.Handle(new GetBestSellingDishesQuery()));

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
        => Ok(await reportQueryService.GetAllTransactions());
}