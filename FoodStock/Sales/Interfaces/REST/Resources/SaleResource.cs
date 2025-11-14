using FoodStock.Sales.Domain.Model.ValueObjects;

namespace FoodStock.Sales.Interfaces.REST.Resources;

public record SaleResource(int Id, ESaleType SaleType, EPaymentMethod PaymentMethod, decimal Total, string Waiter, IReadOnlyList<SaleItemResource> SaleItems);