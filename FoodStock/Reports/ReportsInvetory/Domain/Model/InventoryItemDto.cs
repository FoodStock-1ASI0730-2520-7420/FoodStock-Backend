namespace FoodStock.Reports.ReportsInventory.Domain.Model;

public class InventoryItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}