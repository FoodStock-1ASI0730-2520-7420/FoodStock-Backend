namespace FoodStock.Inventory.Domain.Model.Commands.Dishes;

public record CreateDishCommand(string Name, decimal PriceUnit, IEnumerable<(long ProductId, decimal Quantity)> Ingredients);
