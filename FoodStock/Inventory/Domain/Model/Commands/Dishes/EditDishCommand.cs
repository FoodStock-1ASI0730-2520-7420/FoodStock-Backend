namespace FoodStock.Inventory.Domain.Model.Commands.Dishes;

public record EditDishCommand(long Id, string Name, decimal PriceUnit, IEnumerable<(long ProductId, decimal Quantity)> Ingredients);
