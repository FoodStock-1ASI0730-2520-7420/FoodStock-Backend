using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.ValueObjects;

namespace FoodStock.Sales.Domain.Model.Commands;

public record UpdateSaleCommand(int Id, ESaleType SaleType, EPaymentMethod PaymentMethod, string Waiter, List<UpdateSaleCommand.SaleItemData> SaleItems)
{
    public record SaleItemData(int DishId, string Name, decimal PrinceUnit, int Quantity);
}