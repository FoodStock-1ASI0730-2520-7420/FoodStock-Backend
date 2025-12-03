using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Domain.Model.ValueObjects;

namespace FoodStock.Sales.Interfaces.REST.Resources;

public record UpdateSaleResource(
    int Id,
    ESaleType SaleType,
    EPaymentMethod PaymentMethod,
    string Waiter,
    List<UpdateSaleCommand.SaleItemData> SaleItems);