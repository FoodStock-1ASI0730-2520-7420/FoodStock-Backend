namespace FoodStock.Suppliers.Domain.Model.Commands;

public record CreateSupplierCommand(string Name, string Contact, string Type, string Email);