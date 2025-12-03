namespace FoodStock.Reports.SalesReports.Domain.Model;

public class SalesReportSummary
{
    public decimal TotalRevenue { get; set; }
    public int TotalSales { get; set; }
    public decimal AvgTicket { get; set; }
}