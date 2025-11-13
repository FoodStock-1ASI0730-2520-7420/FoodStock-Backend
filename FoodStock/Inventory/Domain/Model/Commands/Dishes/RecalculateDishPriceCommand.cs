namespace FoodStock.Inventory.Domain.Model.Commands.Dishes;

/// <summary>
/// Command to recalculate the price of a dish based on current product prices.
/// </summary>
public record RecalculateDishPriceCommand(long DishId);