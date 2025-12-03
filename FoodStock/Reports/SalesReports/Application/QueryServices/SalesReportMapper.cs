namespace FoodStock.Reports.SalesReports.Application.QueryServices;


public static class SalesReportMapper
{
    public static decimal SafeDecimal(object? value)
        => value is decimal d ? d : 0;
}
