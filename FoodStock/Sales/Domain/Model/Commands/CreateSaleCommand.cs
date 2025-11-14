using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.ValueObjects;

namespace FoodStock.Sales.Domain.Model.Commands;

public record CreateSaleCommand(ESaleType SaleType, EPaymentMethod PaymentMethod, string Waiter, List<CreateSaleItemCommand> SaleItems);