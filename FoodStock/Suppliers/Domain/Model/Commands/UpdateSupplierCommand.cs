namespace FoodStock.Suppliers.Domain.Model.Commands;

public record UpdateSupplierCommand(int Id, string Name, string Contact, string Type, string Email);