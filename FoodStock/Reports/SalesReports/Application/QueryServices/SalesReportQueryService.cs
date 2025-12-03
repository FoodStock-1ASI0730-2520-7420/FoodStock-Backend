using FoodStock.Sales.Domain.Repositories;
using FoodStock.Reports.SalesReports.Application.Queries;
using FoodStock.Reports.SalesReports.Domain.Model;

namespace FoodStock.Reports.SalesReports.Application.QueryServices;

public class SalesReportQueryService(ISaleRepository saleRepository)
{
    // ------------------------------
    // OVERVIEW
    // ------------------------------
    public async Task<SalesReportSummary> Handle(GetSalesReportQuery query)
    {
        var salesList = (await saleRepository.ListAsync()).ToList();

        var totalRevenue = salesList.Sum(s => s.Total);
        var totalSales = salesList.Count;
        var avg = totalSales > 0 ? totalRevenue / totalSales : 0;

        return new SalesReportSummary
        {
            TotalRevenue = totalRevenue,
            TotalSales = totalSales,
            AvgTicket = avg
        };
    }

    // ------------------------------
    // PAYMENT SUMMARY
    // ------------------------------
    public async Task<IEnumerable<PaymentSummaryDto>> Handle(GetPaymentSummaryQuery query)
    {
        var salesList = (await saleRepository.ListAsync()).ToList();

        return salesList
            .GroupBy(s => s.PaymentMethod.ToString())
            .Select(g => new PaymentSummaryDto(
                Method: g.Key,
                Transactions: g.Count(),
                Total: g.Sum(x => x.Total)
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }

    // ------------------------------
    // BEST SELLING DISHES
    // ------------------------------
    public async Task<IEnumerable<BestSellingDishDto>> Handle(GetBestSellingDishesQuery query)
    {
        var salesList = (await saleRepository.ListAsync()).ToList();
        var saleItems = salesList.SelectMany(s => s.SaleItems).ToList();

        var grouped = saleItems
            .GroupBy(i => i.Name)
            .Select(g => new BestSellingDishDto(
                Name: g.Key,
                DishId: g.First().DishId,
                Category: "N/A",
                Quantity: g.Sum(x => x.Quantity),
                Subtotal: g.Sum(x => x.PriceUnit * x.Quantity),
                SalesPercent: 0
            ))
            .OrderByDescending(x => x.Quantity)
            .ToList();

        var totalQty = grouped.Sum(x => x.Quantity);

        grouped = grouped
            .Select(x => x with
            {
                SalesPercent = totalQty > 0
                    ? Math.Round((decimal)x.Quantity / totalQty * 100, 1)
                    : 0
            })
            .ToList();

        return grouped;
    }

    // ------------------------------
    // TRANSACTIONS (simplificado porque Sale NO tiene CreationDate/CreationTime)
    // ------------------------------
    public async Task<IEnumerable<TransactionDto>> GetAllTransactions()
    {
        var salesList = (await saleRepository.ListAsync()).ToList();

        return salesList.Select(s => new TransactionDto(
            Id: s.Id,
            SaleType: s.SaleType.ToString(),
            PaymentMethod: s.PaymentMethod.ToString(),
            Total: s.Total,
            Waiter: s.Waiter
        ));
    }
}

// ------------------------------------
//           DTOs
// ------------------------------------

public record PaymentSummaryDto(
    string Method,
    int Transactions,
    decimal Total
);

public record BestSellingDishDto(
    string Name,
    long DishId,
    string Category,
    int Quantity,
    decimal Subtotal,
    decimal SalesPercent
);

public record TransactionDto(
    int Id,
    string SaleType,
    string PaymentMethod,
    decimal Total,
    string Waiter
);
