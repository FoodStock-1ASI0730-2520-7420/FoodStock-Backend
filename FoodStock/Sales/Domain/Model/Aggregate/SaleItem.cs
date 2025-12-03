using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Sales.Domain.Model.Commands;

namespace FoodStock.Sales.Domain.Model.Aggregate;

public partial class SaleItem
{
    public SaleItem(long dishId, string name, decimal priceUnit, int quantity, int saleId)
    {
        DishId = dishId;
        Name = name;
        PriceUnit = priceUnit;
        Quantity = quantity;
        SaleId = saleId;
    }

    public SaleItem(CreateSaleItemCommand command, Dish dish, int saleId)
        : this(dish.Id, dish.Name, dish.PriceUnit, command.Quantity, saleId)
    {
    }
    
    public SaleItem(SaleItemDto dto, Dish dish, int saleId)
        : this(dish.Id, dish.Name, dish.PriceUnit, dto.Quantity, saleId)
    {
    }
    
    public int Id { get; }
    public long DishId { get; private set; }
    public string Name { get; private set; }
    public decimal PriceUnit { get; private set; }
    public int Quantity { get; private set; }
    public Sale Sale { get; internal set; }
    public int SaleId { get; private set; }
    
    public record SaleItemDto(int DishId, string Name, decimal PriceUnit, int Quantity);
}