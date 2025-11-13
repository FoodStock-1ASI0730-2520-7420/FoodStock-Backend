namespace FoodStock.Inventory.Domain.Model.Commands.Dishes;
public record EditDishCommand(long Id, string Name, IEnumerable<(long ProductId, decimal Quantity)> Ingredients);