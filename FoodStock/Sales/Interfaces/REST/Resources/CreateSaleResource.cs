using FoodStock.Sales.Domain.Model.ValueObjects;

namespace FoodStock.Sales.Interfaces.REST.Resources;

public record CreateSaleResource(ESaleType SaleType, EPaymentMethod PaymentMethod, string Waiter, List<CreateSaleItemResource> SaleItems);