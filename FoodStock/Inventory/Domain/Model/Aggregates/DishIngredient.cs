public class DishIngredient
{
    protected DishIngredient() { }

    public DishIngredient(long productId, decimal quantity, long dishId)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        ProductId = productId;
        Quantity = quantity;
        DishId = dishId;
    }

    public long Id { get; private set; }
    public long ProductId { get; private set; }
    public decimal Quantity { get; private set; }
    public long DishId { get; private set; } // ✅ nuevo campo
}