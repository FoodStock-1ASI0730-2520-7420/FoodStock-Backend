namespace FoodStock.Inventory.Domain.Model.Commands.Dishes;
public record CreateDishCommand(string Name, IEnumerable<(long ProductId, decimal Quantity)> Ingredients);