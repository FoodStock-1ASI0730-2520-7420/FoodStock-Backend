namespace FoodStock.Reports.ReportsInventory.Domain.Model;

public class InventorySummary
{
    public int TotalProducts { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal TotalValue { get; set; }
}