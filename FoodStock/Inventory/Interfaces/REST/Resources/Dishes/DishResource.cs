namespace FoodStock.Inventory.Interfaces.REST.Resources.Dishes;

public record DishIngredientResource(long ProductId, decimal Quantity);

public record DishResource(long Id, string Name, decimal PriceUnit, IEnumerable<DishIngredientResource> Ingredients);
