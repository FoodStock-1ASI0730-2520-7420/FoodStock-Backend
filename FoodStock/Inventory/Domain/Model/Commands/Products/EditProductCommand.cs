namespace FoodStock.Inventory.Domain.Model.Commands.Products;
public record EditProductCommand(long Id, string Name, decimal UnitPrice, decimal Quantity, DateTime? ExpirationDate, string Category);