namespace FoodStock.Inventory.Domain.Model.Aggregates;

public class Dish
{
    protected Dish() { }

    public Dish(string name, decimal priceUnit)
    {
        Name = name;
        PriceUnit = priceUnit;
        Ingredients = new List<DishIngredient>();
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
    public decimal PriceUnit { get; private set; } // ✅ renombrado

    public List<DishIngredient> Ingredients { get; private set; }

    public void AddIngredients(List<DishIngredient> ingredients)
    {
        Ingredients.AddRange(ingredients);
    }

    public void Edit(string name, decimal priceUnit, List<DishIngredient> ingredients)
    {
        Name = name;
        PriceUnit = priceUnit;
        Ingredients = ingredients;
    }
}