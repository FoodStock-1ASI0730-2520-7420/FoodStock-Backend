using FoodStock.Suppliers.Domain.Model.Commands;
using FoodStock.Suppliers.Interfaces.REST.Resources;

namespace FoodStock.Suppliers.Interfaces.REST.Transform;

public static class UpdateSupplierCommandFromResourceAssembler
{
    public static UpdateSupplierCommand ToCommand(int id, EditSupplierResource resource) =>
        new(id, resource.Name, resource.Contact, resource.Type, resource.Email);
}