namespace FoodStock.Sales.Interfaces.REST.Resources;

public record SaleItemResource(int Id, long DishId, string Name, decimal PriceUnit, int Quantity);