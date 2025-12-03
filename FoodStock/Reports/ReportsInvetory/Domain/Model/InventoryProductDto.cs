namespace FoodStock.Reports.ReportsInventory.Domain.Model;

public class InventoryProductDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalValue { get; set; }
}