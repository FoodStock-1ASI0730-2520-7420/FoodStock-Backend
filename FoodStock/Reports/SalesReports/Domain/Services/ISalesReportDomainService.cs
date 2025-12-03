using FoodStock.Sales.Domain.Model.Aggregate;

namespace FoodStock.Reports.SalesReports.Domain.Services;

public interface ISalesReportDomainService
{
    decimal CalculateTotalRevenue(IEnumerable<Sale> sales);
    int CalculateTotalSales(IEnumerable<Sale> sales);
    decimal CalculateAvgTicket(decimal totalRevenue, int totalSales);
}