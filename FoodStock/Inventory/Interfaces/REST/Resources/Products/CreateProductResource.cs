namespace FoodStock.Inventory.Interfaces.REST.Resources.Products;
public record CreateProductResource(string Name, decimal UnitPrice, decimal Quantity, DateTime? ExpirationDate, string Category);