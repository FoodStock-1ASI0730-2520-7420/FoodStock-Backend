namespace FoodStock.Suppliers.Interfaces.REST.Resources;

public record CreateSupplierResource(string Name, string Contact, string Type, string Email);