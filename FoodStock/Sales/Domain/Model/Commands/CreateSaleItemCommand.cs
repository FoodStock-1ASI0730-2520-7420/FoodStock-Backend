namespace FoodStock.Sales.Domain.Model.Commands;

public record CreateSaleItemCommand(long DishId, int Quantity);