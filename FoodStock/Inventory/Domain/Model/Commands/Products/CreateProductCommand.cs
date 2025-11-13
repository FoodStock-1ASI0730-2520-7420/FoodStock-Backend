namespace FoodStock.Inventory.Domain.Model.Commands.Products;
public record CreateProductCommand(string Name, decimal UnitPrice, decimal Quantity, DateTime? ExpirationDate, string Category);